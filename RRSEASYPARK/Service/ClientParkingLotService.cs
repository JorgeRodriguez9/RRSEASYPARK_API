using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class ClientParkingLotService : IClientParkingLotService
    {
        private readonly RRSEASYPARKContext _context;

        public ClientParkingLotService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddClientParkingLot(string name, long identification, string email, long telephone, Guid idUser)
        {
            try
            {
                await _context.ClientParkingLot.AddAsync(new ClientParkingLot()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Identification = identification,
                    Telephone = telephone,
                    UserId = idUser

                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Client add Correct"
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

        public async Task<ClientParkingLot?> GetClientParkingLot(Guid clientParkingLotId)
        {
            return await _context.ClientParkingLot.FindAsync(clientParkingLotId);
        }

        public async Task<IEnumerable<ClientParkingLot>> GetClientParkingLots()
        {
            return await _context.ClientParkingLot.ToListAsync();
        }     

        public async Task<ServiceResponse> UpdateClientParkingLot(Guid clientParkingLotId, string name, long identification, string email, long telephone)
        {
            try
            {
                var client = await _context.ClientParkingLot.FindAsync(clientParkingLotId);
                if (client == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Client don't exist"
                    };
                }
                client.Name = name;
                client.Email = email;
                client.Identification = identification;
                client.Telephone = telephone;

                _context.ClientParkingLot.Update(client);

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
        public async Task<ServiceResponse?> DeleteClientParkingLot(Guid clientParkingLotId)
        {
            try
            {
                var client = await _context.ClientParkingLot.FindAsync(clientParkingLotId);

                if (client == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Client don't exist"
                    };
                }
                _context.ClientParkingLot.Remove(client);
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
