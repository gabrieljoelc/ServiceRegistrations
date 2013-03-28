namespace Entities
{
    public interface IServiceTypeLookupsRepository
    {
        ServiceLookup GetLatestByType(ServiceType type);
    }
}