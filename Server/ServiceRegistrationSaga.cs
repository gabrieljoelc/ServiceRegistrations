using System;
using System.Linq;
using Entities;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Server
{
    class ServiceRegistrationSaga : Saga<ServiceRegistrationSagaEntity>,

        // what happens to if a TurnOnServiceRegistration message comes before it's corresponding
        // RequestServiceRegistration (see http://support.nservicebus.com/customer/portal/articles/860458-sagas-in-nservicebus)?
        IAmStartedByMessages<RequestServiceRegistration>,
        IHandleMessages<TurnOnServiceRegistration>,
        
        IHandleTimeouts<IServiceRegistrationExpirationReminderTimeout>,
        IHandleTimeouts<IServiceRegistrationExpirationTimeout>
    {
        public IServiceTypeLookupsRepository ServiceTypeLookupsRepository { get; set; }
        protected IServiceRegistrationRepository ServiceRegistrationRepository { get; set; }
        
        public void Handle(RequestServiceRegistration message)
        {
            var serviceRegistration =
                ServiceRegistrationRepository.GetByPersonId(message.PersonId)
                                             .SingleOrDefault(x => x.ServiceType == message.Type);
            // if the service registration exists, then ignore the command (no throws)
            if (serviceRegistration != null)
            {
                return;
            }
            Data.ServiceRegistrationId = Guid.NewGuid();
            Data.PersonId = message.PersonId;
            Data.ServiceType = message.Type;
        }

        public void Handle(TurnOnServiceRegistration message)
        {
            // if the it's on and not renewable, then ignore the command (no throws)
            if (Data.IsOn && !Data.IsRenewable) return;
            // this would only be for newly created registrations or one's that are already on and need to be renewed
            
            // get relevant dates for this service type
            var svcTypeLookup = ServiceTypeLookupsRepository.GetLatestByType(Data.ServiceType);
            Data.ExpirationReminderDate = svcTypeLookup.ExpirationReminderDate;
            Data.ExpirationDate = svcTypeLookup.ExpirationDate;

            if (Data.ExpirationReminderDate.HasValue)
            {
                RequestUtcTimeout<IServiceRegistrationExpirationReminderTimeout>(Data.ExpirationDate.Value, x =>
                    {
                        x.ExpirationReminderDate = Data.ExpirationReminderDate.Value;
                    });
            }
            if (Data.ExpirationDate.HasValue)
            {
                RequestUtcTimeout<IServiceRegistrationExpirationTimeout>(Data.ExpirationDate.Value, x =>
                {
                    x.ExpirationDate = Data.ExpirationDate.Value;
                });
            }
            Data.IsOn = true;
        }

        public void Timeout(IServiceRegistrationExpirationReminderTimeout state)
        {
            // ignore stale timeout
            // "your code needs to be prepared to discard "invalid" messages no matter what" (http://stackoverflow.com/a/13247509/34315)
            if (Data.ExpirationReminderDate != state.ExpirationReminderDate)
            {
                return;
            }
            Bus.SendLocal(new SendServiceRegistrationExpirationReminder
            {
                PersonId = Data.PersonId,
                ServiceType = Data.ServiceType
            });
        }
        
        public void Timeout(IServiceRegistrationExpirationTimeout state)
        {
            // ignore stale timeout
            // "your code needs to be prepared to discard "invalid" messages no matter what" (http://stackoverflow.com/a/13247509/34315)
            if (Data.ExpirationDate != state.ExpirationDate)
            {
                return;
            }
            Data.IsOn = false;
            if (!Data.IsRenewable)
            {
                MarkAsComplete();
            }
            Bus.SendLocal(new SendServiceRegistrationExpiredNotice
                {
                    PersonId = Data.PersonId,
                    ServiceType = Data.ServiceType
                });
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<TurnOnServiceRegistration>(@event => @event.ServiceRegistrationId,
                                                        data => data.ServiceRegistrationId);
        }
    }
}
