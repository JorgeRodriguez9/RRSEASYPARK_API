using RRSEasyPark.Models;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public interface IReservationService
    {
        Task<ServiceResponse> AddReservation(string date, long totalprice, TimeOnly starttime, TimeOnly endtime, Guid typeVehicleId, Guid parkingLotId, string Disability);
        Task<Reservation?> GetReservation(Guid ReservationId);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<ServiceResponse> UpdateReservation(Guid ReservationId, string date,TimeOnly starttime, TimeOnly enddate, long totalPrice, string disabled);
        Task<ServiceResponse?> DeleteReservation(Guid ReservationId);
    }
}
