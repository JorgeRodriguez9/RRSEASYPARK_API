using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface ITypeVehiculeService
    {
        Task<ServiceResponse> AddTypeVehicle(string name);
        Task<TypeVehicle?> GetTypeVehicle(Guid TypeVehicleId);
        Task<IEnumerable<TypeVehicle>> GetTypeVehicles();
        Task<ServiceResponse> UpdateTypeVehicle(Guid TypeVehicleId, string name);
        Task<ServiceResponse?> DeleteTypeVehicle(Guid TypeVehicleId);

    }
}
