using System;
using NServiceBus.Saga;

namespace Entities
{
    public class ServiceRegistrationSagaEntity : IContainSagaData, IServiceRegistration
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        [Unique]
        public Guid ServiceRegistrationId { get; set; }
        public ServiceType ServiceType { get; set; }
        public Guid PersonId { get; set; }

        // Access this directly from repository
        public bool IsOn { get; set; }

        public bool IsRenewable { get; set; }

        public DateTime? ExpirationDate { get; set; }
        public DateTime? ExpirationReminderDate { get; set; }
    }
}