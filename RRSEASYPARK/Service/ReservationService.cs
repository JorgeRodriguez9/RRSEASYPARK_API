using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;
using System.Linq;

namespace RRSEASYPARK.Service
{
    public class ReservationService : IReservationService
    {
        private readonly RRSEASYPARKContext _context;

        public ReservationService(RRSEASYPARKContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> AddReservation(string date, long totalprice,TimeOnly starttime, TimeOnly endtime, Guid typeVehicleId, Guid parkingLotId, string disability)
        {
            try
            {
                //TimeZoneInfo colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); // Colombia's time zone

                var ParkingLot = await _context.parkingLots.FindAsync(parkingLotId);
                var TypeVehicle = await _context.typeVehicles.FindAsync(typeVehicleId);
                

                if (ParkingLot == null)
                {
                    throw new Exception("ParkingLot is null");
                }

                if (TypeVehicle == null)
                {
                    throw new Exception("TypeVehicle is null");
                }



                await _context.reservations.AddAsync(new Reservation()
                {
                    Id = Guid.NewGuid(),
                    //Date = TimeZoneInfo.ConvertTime(date, colombiaTimeZone),
                    Date = date.ToString(),
                    StartTime =starttime.ToString(),
                    EndTime = endtime.ToString(),
                    TypeVehicleId = typeVehicleId,
                    ParkingLotId = parkingLotId,
                    Disabled = disability,
                    TotalPrice = totalprice,
                    ClientParkingLotId = Guid.Parse("847cafcf-dac7-48b0-935d-018b8d0de1fa")
                });

                switch (TypeVehicle.Name)
                {
                    case "Carro":                        
                        if (disability == Enums.DisabilityValues.SI.ToString())
                        {
                            //ParkingLot.CantSpacesCar += (int)Enums.NumbersValues.b;
                            ParkingLot.CantSpacesDisability -= (int)Enums.NumbersValues.b;
                            break;
                        }
                        ParkingLot.CantSpacesCar -= (int)Enums.NumbersValues.b;
                        break;
                    case "Moto":
                        ParkingLot.CantSpacesMotorcycle -= (int)Enums.NumbersValues.b;
                        break;
                    default:
                        Console.WriteLine("Unrecognized vehicle type");
                        break;
                }

                _context.parkingLots.Update(ParkingLot);

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
            return await _context.reservations.Include(x => x.ClientParkingLot).ToListAsync();
        }
        public async Task<ServiceResponse> UpdateReservation(Guid ReservationId, string date,TimeOnly startTime, TimeOnly endTime, long totalPrice, string disabled)
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

                reservation.Date = date.ToString();
                reservation.StartTime = startTime.ToString();
                reservation.EndTime = endTime.ToString();
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
