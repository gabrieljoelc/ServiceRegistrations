using System;
using System.Linq;
using Entities;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Server
{
    // this could be an option if we really didn't want to ever lookup saga entities for UI authorization
    //class ServiceRegistrationSagaForNonSagaEntity : Saga<ServiceRegistrationSagaForNonSagaEntityData>,
    //    IAmStartedByMessages<RequestServiceRegistration>,
    //    IHandleMessages<TurnOnServiceRegistration>,
    //    IHandleTimeouts<ServiceRegistrationExpirationTimeout>
    //{
    //    public IServiceTypeLookupsRepository ServiceTypeLookupsRepository { get; set; }
    //    protected IServiceRegistrationRepository ServiceRegistrationRepository { get; set; }

    //    public void Handle(RequestServiceRegistration message)
    //    {
    //        var serviceRegistration =
    //            ServiceRegistrationRepository.GetByPersonId(message.PersonId)
    //                                         .SingleOrDefault(x => x.ServiceType == message.Type);
    //        if (serviceRegistration != null)
    //        {
    //            return;
    //        }

    //        var svcRegistration = new ServiceRegistration
    //        {
    //            Id = Guid.NewGuid(),
    //            PersonId = message.PersonId,
    //            ServiceType = message.Type,
    //            IsOn = false
    //        };
    //        Data.ServiceRegistrationId = svcRegistration.Id;
    //    }

    //    public void Handle(TurnOnServiceRegistration message)
    //    {
    //        var serviceRegistration = ServiceRegistrationRepository.Get(message.ServiceRegistrationId);
    //        if (serviceRegistration.IsOn && !serviceRegistration.IsRenewable) return;

    //        serviceRegistration.IsOn = true;

    //        // get relevant dates for this service type
    //        var svcTypeLookup = ServiceTypeLookupsRepository.GetLatestByType(serviceRegistration.ServiceType);

    //        // What if this command is received for a renewable svc reg that is already on before the
    //        // original timeouts expire? Does this code reset those ones? If not, how do you cancel them?

    //        if (svcTypeLookup.WeeksFromExpiration.HasValue)
    //        {
    //            RequestUtcTimeout<ServiceRegistrationExpirationReminderTimeout>(svcTypeLookup.ExpirationReminderDate.Value, x =>
    //            {
    //                x.ServiceType = serviceRegistration.ServiceType;
    //                x.PersonId = serviceRegistration.PersonId;
    //            });
    //        }
    //        if (svcTypeLookup.MonthsToExpiration.HasValue)
    //        {
    //            RequestUtcTimeout<ServiceRegistrationExpirationTimeout>(svcTypeLookup.ExpirationDate.Value, x =>
    //            {
    //                x.ServiceType = serviceRegistration.ServiceType;
    //                x.PersonId = serviceRegistration.PersonId;
    //            });
    //        }
    //    }

    //    public void Timeout(ServiceRegistrationExpirationTimeout state)
    //    {
    //        var serviceRegistration = ServiceRegistrationRepository.Get(Data.ServiceRegistrationId);
    //        serviceRegistration.IsOn = false;
    //        if (!serviceRegistration.IsRenewable)
    //        {
    //            MarkAsComplete();
    //        }
    //    }

    //    public override void ConfigureHowToFindSaga()
    //    {
    //        ConfigureMapping<TurnOnServiceRegistration>(@event => @event.ServiceRegistrationId, data => data.ServiceRegistrationId);
    //    }
    //}
}