using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IValueService
    {

        Task<ServiceResponse> AddValue(decimal valueHC);
        Task<Value?> GetValue(Guid valueId);
        Task<IEnumerable<Value>> GetValues();
        Task<ServiceResponse> UpdateValue(Guid valueId, decimal valueHC);
        Task<ServiceResponse?> DeleteValue(Guid valueId);

    }
}
