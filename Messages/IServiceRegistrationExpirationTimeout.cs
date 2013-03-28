using System;

namespace Messages
{
    public interface IServiceRegistrationExpirationTimeout
    {
        DateTime ExpirationDate { get; set; }
    }
}