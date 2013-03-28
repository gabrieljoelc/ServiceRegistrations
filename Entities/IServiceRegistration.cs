using System;

namespace Entities
{
    public interface IServiceRegistration
    {
        Guid Id { get; set; }
        Guid PersonId { get; set; }
        ServiceType ServiceType { get; set; }
        bool IsOn { get; set; }
        bool IsRenewable { get; set; }
    }
}