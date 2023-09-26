using RRSEASYPARK.Models;
using Microsoft.EntityFrameworkCore;
using RRSEASYPARK.DAL;
using System.Diagnostics.Metrics;
using RRSEasyPark.Models;

namespace RRSEASYPARK.Service
{
    public class CityService : ICityService
    {
        private readonly RRSEASYPARKContext _context;

        public CityService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddCity(string name)
        {
            try
            {
                await _context.Cities.AddAsync(new City()
                {
                    Id = Guid.NewGuid(),
                    Name = name
                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "City add Correct"
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

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetCity(Guid cityId)
        {
            return await _context.Cities.FindAsync(cityId);
        }

        public async Task<ServiceResponse> UpdateCity(Guid cityId, string name)
        {
            try
            {
                var city = await _context.Cities.FindAsync(cityId);
                if (city == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "City don't exist"
                    };
                }
                city.Name = name;
                _context.Cities.Update(city);

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
        public async Task<ServiceResponse?> DeleteCity(Guid cityId)
        {
            try
            {
                var city = await _context.Cities.FindAsync(cityId);

                if (city == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "City don't exist"
                    };
                }
                _context.Cities.Remove(city);
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
