using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IReservationService
    {
        Task<ServiceResponse> AddReservation(string date, long totalprice, TimeOnly starttime, TimeOnly endtime, Guid typeVehicleId, Guid parkingLotId, string Disability, Guid idUser);
        Task<Reservation?> GetReservation(Guid ReservationId);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetReservationsClient(Guid idUser);
        Task<IEnumerable<Reservation>> GetReservationsParkingLot(Guid parkingLotId);
        Task<ServiceResponse> UpdateReservation(Guid ReservationId, string date,string starttime, string enddate, long totalPrice, string disabled);
        Task<ServiceResponse?> DeleteReservation(Guid ReservationId);
    }
}
