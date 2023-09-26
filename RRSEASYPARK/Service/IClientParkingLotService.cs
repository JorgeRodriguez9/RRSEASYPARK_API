using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IClientParkingLotService
    {

        Task<ServiceResponse> AddClientParkingLot(string name, long identification, string email, long telephone, Guid idUser);
        Task<ClientParkingLot?> GetClientParkingLot(Guid clientParkingLotId);
        Task<IEnumerable<ClientParkingLot>> GetClientParkingLots();
        Task<ServiceResponse> UpdateClientParkingLot(Guid clientParkingLotId, string name, long identification, string email, long telephone);
        Task<ServiceResponse?> DeleteClientParkingLot(Guid clientParkingLot);

    }
}
