using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface ICityService
    {

        Task<ServiceResponse> AddCity(string name);
        Task<City?> GetCity(Guid cityId); 
        Task<IEnumerable<City>> GetCities();
        Task<ServiceResponse> UpdateCity(Guid cityId,string name);
        Task<ServiceResponse?> DeleteCity(Guid cityId);

    }
}
