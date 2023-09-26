using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IReservationService
    {
        Task<ServiceResponse> AddReservation(DateTime date, long totalPrice, string disabled, Guid clientId, Guid typeVehicleId, Guid parkingLotId);
        Task<Reservation?> GetReservation(Guid ReservationId);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<ServiceResponse> UpdateReservation(Guid ReservationId, DateTime date, long totalPrice, string disabled);
        Task<ServiceResponse?> DeleteReservation(Guid ReservationId);
    }
}
