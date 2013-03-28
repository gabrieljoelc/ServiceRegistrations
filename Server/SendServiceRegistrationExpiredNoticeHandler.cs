using System;
using Messages;
using NServiceBus;

namespace Server
{
    class SendServiceRegistrationExpiredNoticeHandler : IHandleMessages<SendServiceRegistrationExpiredNotice>
    {
        public void Handle(SendServiceRegistrationExpiredNotice message)
        {
            // TODO: get person by personid and send email to person for specified service type
            throw new NotImplementedException();
        }
    }
}