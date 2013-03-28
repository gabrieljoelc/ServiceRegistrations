using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    //public class ServiceRegistration : IEntity, IServiceRegistration
    //{
    //    public Guid Id { get; set; }
    //    public Guid PersonId { get; set; }
    //    public ServiceType ServiceType { get; set; }
    //    public bool IsOn { get; set; }
    //    public bool IsRenewable { get; set; }
    //}

    public enum ServiceType
    {
        Service1,
        Service2,
        Service3
    }

    public class Person : IEntity
    {
        public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }

    public interface IEntity
    {
    }
}
