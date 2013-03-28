using System;
using System.Collections.Generic;

namespace Entities
{
    public interface IServiceRegistrationRepository
    {
        IServiceRegistration Get(Guid id);
        bool IsOn(Guid personId, ServiceType type);
        IEnumerable<IServiceRegistration> GetByPersonId(Guid personId);
    }
}