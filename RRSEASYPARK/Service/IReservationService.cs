using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IReservationService
    {
        Task<ServiceResponse> AddReservation(DateTime startdate, long totalprice, DateTime enddate, Guid typeVehicleId, Guid parkingLotId, string Disability);
        Task<Reservation?> GetReservation(Guid ReservationId);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<ServiceResponse> UpdateReservation(Guid ReservationId, DateTime stsrtdate, DateTime enddate, long totalPrice, string disabled);
        Task<ServiceResponse?> DeleteReservation(Guid ReservationId);
    }
}
