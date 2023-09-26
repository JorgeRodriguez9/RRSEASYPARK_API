using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class ValueService : IValueService
    {
        private readonly RRSEASYPARKContext _context;

        public ValueService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddValue(decimal valueHC)
        {
            try
            {
                await _context.values.AddAsync(new Value()
                {
                    Id = Guid.NewGuid(),
                    ValueHC = valueHC
                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Value add Correct"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Failed,
                    ErrorMessage = ex.Message
                };
            };
        }

        public async Task<Value?> GetValue(Guid valueId)
        {
            return await _context.values.FindAsync(valueId);
        }

        public async Task<IEnumerable<Value>> GetValues()
        {
            return await _context.values.ToListAsync();
        }

        public async Task<ServiceResponse> UpdateValue(Guid valueId, decimal valueHC)
        {
            try
            {
                var values = await _context.values.FindAsync(valueId);
                if (values == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Values don't exist"
                    };
                }
                values.ValueHC = valueHC;
                _context.values.Update(values);

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
        public async Task<ServiceResponse?> DeleteValue(Guid valueId)
        {
            try
            {
                var values = await _context.values.FindAsync(valueId);

                if (values == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Values don't exist"
                    };
                }
                _context.values.Remove(values);
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
