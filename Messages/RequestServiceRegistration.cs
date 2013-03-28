using System;
using Entities;
using NServiceBus;

namespace Messages
{
    public class RequestServiceRegistration : ICommand
    {
        public Guid PersonId { get; set; }

        public ServiceType Type { get; set; }
    }
}
