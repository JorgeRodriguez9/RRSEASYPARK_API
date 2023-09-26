using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IRolService
    {
        Task<ServiceResponse> AddRol(string name);
        Task<Rol?> GetRol(Guid RolId);
        Task<IEnumerable<Rol>> GetRols();
        Task<ServiceResponse> UpdateRol(Guid RolId, string name);
        Task<ServiceResponse?> DeleteRol(Guid RolId);

    }
}
