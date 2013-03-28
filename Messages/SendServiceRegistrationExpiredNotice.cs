using System;
using Entities;
using NServiceBus;

namespace Messages
{
    public class SendServiceRegistrationExpiredNotice : ICommand
    {
        public Guid PersonId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}