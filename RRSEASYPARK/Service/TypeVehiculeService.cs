using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class TypeVehiculeService : ITypeVehiculeService
    {
        private readonly RRSEASYPARKContext _context;

        public TypeVehiculeService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddTypeVehicle(string name)
        {
            try
            {
                await _context.typeVehicles.AddAsync(new TypeVehicle()
                {
                    Id = Guid.NewGuid(),
                    Name = name
                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Type Vehicle add Correct"
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

        public async Task<TypeVehicle?> GetTypeVehicle(Guid TypeVehicleId)
        {
            return await _context.typeVehicles.FindAsync(TypeVehicleId);
        }

        public async Task<IEnumerable<TypeVehicle>> GetTypeVehicles()
        {
            return await _context.typeVehicles.ToListAsync();
        }

        public async Task<ServiceResponse> UpdateTypeVehicle(Guid TypeVehicleId, string name)
        {
            try
            {
                var typeVehicle = await _context.typeVehicles.FindAsync(TypeVehicleId);
                if (typeVehicle == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Type vehicle don't exist"
                    };
                }
                typeVehicle.Name = name;
                _context.typeVehicles.Update(typeVehicle);

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

        public async Task<ServiceResponse?> DeleteTypeVehicle(Guid TypeVehicleId)
        {
            try
            {
                var typeVehicle = await _context.typeVehicles.FindAsync(TypeVehicleId);

                if (typeVehicle == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Type vehicle don't exist"
                    };
                }
                _context.typeVehicles.Remove(typeVehicle);
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
