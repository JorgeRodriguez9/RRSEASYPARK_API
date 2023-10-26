using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IPropietaryParkService
    {

        Task<ServiceResponse> AddPropietaryPark(string name, long identification, string email, long telephone, Guid idUser);
        Task<PropietaryPark?> GetPropietaryPark(Guid propietaryParkId);
        Task<IEnumerable<PropietaryPark>> GetPropietaryParks();
        Task<ServiceResponse> UpdatePropietaryPark(Guid propietaryParkId, string name, long identification, string email);
        Task<ServiceResponse?> DeletePropietaryPark(Guid propietaryParkId);

    }
}
