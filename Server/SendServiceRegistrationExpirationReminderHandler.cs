using System;
using Messages;
using NServiceBus;

namespace Server
{
    class SendServiceRegistrationExpirationReminderHandler : IHandleMessages<SendServiceRegistrationExpirationReminder>
    {
        public void Handle(SendServiceRegistrationExpirationReminder message)
        {
            // TODO: get person by personid and send email to person for specified service type
            throw new NotImplementedException();
        }
    }
}