using AutoMapper;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Common;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models.Request;
using RRSEASYPARK.Models.Response;
using RRSEASYPARK.Models.ViewModel;
using RRSEASYPARK.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace RRSEASYPARK.Service
{
    public class UserService : IUserService
    {
        private readonly RRSEASYPARKContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(RRSEASYPARKContext context, IOptions<AppSettings> appSettings, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ServiceResponse> AddUser(string name, string password, string rol, Guid id)
        {
      
            try
            {
                var rolId = new Rol();
                var ProPar = await _context.rols.FirstOrDefaultAsync();
                

                if (rol == "Propietary Park")
                {
                    rolId = ProPar;
                }

                else
                {
                    rolId = await _context.rols.Skip(1).FirstOrDefaultAsync();
                }

                string spassword = Encrypt.GetSHA256(password);

                await _context.users.AddAsync(new User()

                {
                    Id = id,
                    Name = name,
                    Password = spassword,
                    RolId = rolId.Id
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

        public async Task<UserResponse> Auth(AuthRequest model)
        {

            UserResponse userresponse = new UserResponse();

            string spassword = Encrypt.GetSHA256(model.Password);

            var user = await _context.users.Where(c => c.Name == model.nameUser && c.Password == spassword).Include(x => x.Rol).FirstOrDefaultAsync();

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
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

        public async Task<ServiceResponse> StarRecovery(RecoveryViewModel model)
        {
            try
            {
                string token = Encrypt.GetSHA256(Guid.NewGuid().ToString());

                var user = await _context.propietaryParks.Where(d => d.Email == model.Email).FirstOrDefaultAsync();
                var userClient = await _context.ClientParkingLot.Where(d => d.Email == model.Email).FirstOrDefaultAsync();

                if (user != null)
                {
                    var us = await _context.users.Where(d => d.Id == user.UserId).FirstOrDefaultAsync();

                    us.Token_Recovery = token;

                    _context.users.Update(us);

                    await _context.SaveChangesAsync();

                    //send email

                    SendEmail(model.Email, token);

                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Succeded

                    };
                }
                if (userClient != null)
                {
                    var us = await _context.users.Where(d => d.Id == userClient.UserId).FirstOrDefaultAsync();

                    us.Token_Recovery = token;

                    _context.users.Update(us);

                    await _context.SaveChangesAsync();

                    //send email

                    SendEmail(model.Email, token);

                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Succeded

                    };
                }

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = "Correo No existe"
                };
            }
            catch (Exception ex) {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message

                };

            }

        }

        public async Task<ServiceResponse> RecoveryPassword(RecoveryPasswordViewModel model)
        {
            try
            {
                var user = await _context.users.Where(d => d.Token_Recovery == model.token).FirstOrDefaultAsync();
                string spassword = Encrypt.GetSHA256(model.Password);

                if (user != null)
                {
                    user.Password = spassword;
                    user.Token_Recovery = null;
                    _context.users.Update(user);
                    await _context.SaveChangesAsync();
                }

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

        public async Task<ServiceResponse> ValidationToken(string token)
        {
            try
            {
                var user = await _context.users.Where(d => d.Token_Recovery == token).FirstOrDefaultAsync();
                if(user == null || token == null || token.Trim().Equals(""))
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Error",
                        InformationMessage = "Token Expirado"
                    };
                }

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded

                };

            }
            catch (Exception ex) {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message

                };
            }
        }

        public void SendEmail(string destinyEmail, string token)
        {
           

            string OriginEmail = _config.GetSection("Email:UserName").Value;
            string password = _config.GetSection("Email:Password").Value;
            string url = _config.GetSection("Domain:Url").Value + "ConfirmPassword/" + token;


            MailMessage oMailMessage = new MailMessage(OriginEmail, destinyEmail, "Recuperacion de Contraseña",
                "<p>Hola, como estas, esperemos que bien, te enviamos este correo para recuperación de la contraseña que solicitaste, entra al siguiente link:</p><br>" + 
                "<a href='"+url+"'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(_config.GetSection("Email:Host").Value);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Port = int.Parse(_config.GetSection("Email:Port").Value); 
            smtpClient.Credentials = new System.Net.NetworkCredential(OriginEmail, password);

            smtpClient.Send(oMailMessage);

            smtpClient.Dispose();

        }

    }
}
