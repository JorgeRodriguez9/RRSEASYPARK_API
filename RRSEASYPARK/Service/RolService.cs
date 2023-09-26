using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class RolService : IRolService
    {
        private readonly RRSEASYPARKContext _context;

        public RolService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddRol(string name)
        {
            try
            {
                await _context.rols.AddAsync(new Rol()
                {
                    Id = Guid.NewGuid(),
                    Name = name
                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Rol add Correct"
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

        public async Task<Rol?> GetRol(Guid RolId)
        {
            return await _context.rols.FindAsync(RolId);
        }

        public async Task<IEnumerable<Rol>> GetRols()
        {
            return await _context.rols.ToListAsync();
        }

        public async Task<ServiceResponse> UpdateRol(Guid RolId, string name)
        {
            try
            {
                var rol = await _context.rols.FindAsync(RolId);
                if (rol == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Rol don't exist"
                    };
                }
                rol.Name = name;
                _context.rols.Update(rol);

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

        public async Task<ServiceResponse?> DeleteRol(Guid RolId)
        {
            try
            {
                var rol = await _context.rols.FindAsync(RolId);

                if (rol == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Rol don't exist"
                    };
                }
                _context.rols.Remove(rol);
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
