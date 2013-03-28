using System;
using Entities;
using NServiceBus;

namespace Messages
{
    public class SendServiceRegistrationExpirationReminder : ICommand
    {
        public Guid PersonId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}