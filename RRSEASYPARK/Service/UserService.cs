using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Common;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models.Request;
using RRSEASYPARK.Models.Response;
using RRSEASYPARK.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RRSEASYPARK.Service
{
    public class UserService : IUserService
    {
        private readonly RRSEASYPARKContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(RRSEASYPARKContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
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

        public UserResponse Auth(AuthRequest model)
        {

            UserResponse userresponse = new UserResponse();

            string spassword = Encrypt.GetSHA256(model.Password);

            var user = _context.users.Where(c => c.Name == model.nameUser && c.Password == spassword).Include(x => x.Rol).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var userRequest = _mapper.Map<User, UserRequest>(user);

            userresponse.UserName = user.Name;
            userresponse.Token = GetToken(userRequest);
           

            return userresponse;
           
        }

        private string GetToken(UserRequest user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(

                    new Claim[]
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("RolId", user.RolId.ToString()),
                        new Claim(ClaimTypes.Role, user.RolName.ToString()),

                    }

                    ),

                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
