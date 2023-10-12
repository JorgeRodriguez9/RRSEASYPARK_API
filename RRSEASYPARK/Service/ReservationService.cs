using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;

namespace RRSEASYPARK.Service
{
    public class ReservationService : IReservationService
    {
        private readonly RRSEASYPARKContext _context;

        public ReservationService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddReservation(DateTime startdate, long totalprice, DateTime enddate, Guid typeVehicleId, Guid parkingLotId, string Disability )
        {
            try
            {
                TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); // Colombia's time zone

                await _context.reservations.AddAsync(new Reservation()
                {
                    Id = Guid.NewGuid(),
                    StartDate = TimeZoneInfo.ConvertTime(startdate, colombiaTimeZone),
                    EndDate = TimeZoneInfo.ConvertTime(enddate, colombiaTimeZone),
                    TypeVehicleId = typeVehicleId,
                    ParkingLotId = parkingLotId,
                    Disabled = Disability,
                    TotalPrice = totalprice,
                    ClientParkingLotId = Guid.Parse("847cafcf-dac7-48b0-935d-018b8d0de1fa")

                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "Reservation add Correct"
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

        public async Task<Reservation?> GetReservation(Guid ReservationId)
        {
            return await _context.reservations.FindAsync(ReservationId);
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _context.reservations.ToListAsync();
        }
        public async Task<ServiceResponse> UpdateReservation(Guid ReservationId, DateTime startdate, DateTime enddate, long totalPrice, string disabled)
        {
            try
            {
                var reservation = await _context.reservations.FindAsync(ReservationId);
                if (reservation == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Reservation don't exist"
                    };
                }

                reservation.StartDate = startdate;
                reservation.EndDate = enddate;
                reservation.TotalPrice = totalPrice;
                reservation.Disabled = disabled;

                _context.reservations.Update(reservation);

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
        public async Task<ServiceResponse?> DeleteReservation(Guid ReservationId)
        {
            try
            {
                var reservation = await _context.reservations.FindAsync(ReservationId);

                if (reservation == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Reservation don't exist"
                    };
                }
                _context.reservations.Remove(reservation);
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
