using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class UserService : IUserService
    {
        private readonly RRSEASYPARKContext _context;

        public UserService(RRSEASYPARKContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> AddUser(string name, string password, Guid RolId)
        {
            try
            {
                await _context.users.AddAsync(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Password = password,
                    RolId = RolId
                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "User add Correct"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<User?> GetUser(Guid UserId)
        {
            return await _context.users.FindAsync(UserId);
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<ServiceResponse> UpdateUser(Guid UserId, string name, string password)
        {
            try
            {
                var user = await _context.users.FindAsync(UserId);
                if (user == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "user don't exist"
                    };
                }
                user.Name = name;
                user.Password = password;
                _context.users.Update(user);

                await _context.SaveChangesAsync();
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message

                };
            }
        }

        public async Task<ServiceResponse?> DeleteUser(Guid UserId)
        {
            try
            {
                var user = await _context.users.FindAsync(UserId);

                if (user == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "user don't exist"
                    };
                }
                _context.users.Remove(user);
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded

                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
