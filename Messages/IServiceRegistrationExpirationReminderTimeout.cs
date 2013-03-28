using System;

namespace Messages
{
    public interface IServiceRegistrationExpirationReminderTimeout
    {
        DateTime ExpirationReminderDate { get; set; }
    }
}