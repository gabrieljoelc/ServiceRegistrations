using System;
using NServiceBus;

namespace Messages
{
    // could be sent for free service 
    public class TurnOnServiceRegistration : ICommand, IHaveServiceRegistrationId
    {
        public Guid ServiceRegistrationId { get; set; }
    }
}