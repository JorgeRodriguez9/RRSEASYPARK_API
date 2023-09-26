using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IUserService
    {

        Task<ServiceResponse> AddUser(string name, string password, Guid RolId);
        Task<User?> GetUser(Guid UserId);
        Task<IEnumerable<User>> GetUser();
        Task<ServiceResponse> UpdateUser(Guid UserId, string name, string password);
        Task<ServiceResponse?> DeleteUser(Guid UserId);


    }
}
