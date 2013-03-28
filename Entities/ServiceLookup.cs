using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ServiceLookup : IValueObject
    {
        public ServiceType ServiceType { get; set; }
        public int? MonthsToExpiration { get; set; }
        public int? WeeksFromExpiration { get; set; }
        public bool RequiresPayment { get; set; }

        [NotMapped]
        public DateTime? ExpirationDate
        {
            get
            {
                return MonthsToExpiration.HasValue
                           ? (DateTime?) DateTime.Now.AddMonths(MonthsToExpiration.Value)
                           : null;
            }
        }

        [NotMapped]
        public DateTime? ExpirationReminderDate
        {
            get
            {
                return WeeksFromExpiration.HasValue && ExpirationDate.HasValue
                           ? (DateTime?) ExpirationDate.Value.Subtract(TimeSpan.FromDays(7*WeeksFromExpiration.Value))
                           : null;
            }
        }
    }

    public interface IValueObject
    {
    }
}