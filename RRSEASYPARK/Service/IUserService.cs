using RRSEasyPark.Models;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models.Request;
using RRSEASYPARK.Models.Response;
using RRSEASYPARK.Models.ViewModel;

namespace RRSEASYPARK.Service
{
    public interface IUserService
    {

        Task<ServiceResponse> AddUser(string name, string password, string rol, Guid id);
        Task<User?> GetUser(Guid UserId);
        Task<IEnumerable<User>> GetUser();
        Task<ServiceResponse> UpdateUser(Guid UserId, string name, string password);
        Task<ServiceResponse?> DeleteUser(Guid UserId);
        Task<UserResponse> Auth(AuthRequest model);
        Task<ServiceResponse> StarRecovery(RecoveryViewModel model);
        Task<ServiceResponse> RecoveryPassword(RecoveryPasswordViewModel model);
        public Task<ServiceResponse> ValidationToken(string token);

    }
}
