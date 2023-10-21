using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class PropietaryParkService : IPropietaryParkService
    {
        private readonly RRSEASYPARKContext _context;
        public PropietaryParkService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddPropietaryPark(string name, long identification, string email, Guid idUser)
        {
            try
            {
                await _context.propietaryParks.AddAsync(new PropietaryPark()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Identification = identification,
                    UserId = idUser

                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Propietary park add Correct"
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

       

        public async Task<PropietaryPark?> GetPropietaryPark(Guid propietaryParkId)
        {

            return await _context.propietaryParks.FindAsync(propietaryParkId);
        }

        public async Task<IEnumerable<PropietaryPark>> GetPropietaryParks()
        {
            return await _context.propietaryParks.ToListAsync();
        }

        public async Task<ServiceResponse> UpdatePropietaryPark(Guid propietaryParkId, string name, long identification, string email)
        {
            try
            {
                var propietaryPark = await _context.propietaryParks.FindAsync(propietaryParkId);
                if (propietaryPark == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Propietary park don't exist"
                    };
                }
                propietaryPark.Name = name;
                propietaryPark.Email = email;
                propietaryPark.Identification = identification;

                _context.propietaryParks.Update(propietaryPark);

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

        public async Task<ServiceResponse?> DeletePropietaryPark(Guid propietaryParkId)
        {
            try
            {
                var propietaryPark = await _context.propietaryParks.FindAsync(propietaryParkId);

                if (propietaryPark == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Propietary park don't exist"
                    };
                }
                _context.propietaryParks.Remove(propietaryPark);
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
