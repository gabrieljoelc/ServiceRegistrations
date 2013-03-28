using System;

namespace Messages
{
    public interface IHaveServiceRegistrationId
    {
        Guid ServiceRegistrationId { get; set; }
    }
}