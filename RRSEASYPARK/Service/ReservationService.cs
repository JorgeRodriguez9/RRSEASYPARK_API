using FluentAssertions.Common;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Enums;
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
        public async Task<ServiceResponse> AddReservation(string date, long totalprice, TimeOnly starttime, TimeOnly endtime, Guid typeVehicleId, Guid parkingLotId, string disability)
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

                //Variables
                var test = (int)Enums.NumbersValues.b;
                
                DateTime requestedDate = DateTime.ParseExact(date, "MM-dd-yyyy", null); //28
                DateOnly requestedDateOnly = DateOnly.FromDateTime(requestedDate);
                DateTime currentDate = DateTime.Now;                
                DateOnly CurrentDateOnly = DateOnly.FromDateTime(currentDate); //25

                TimeOnly CurrentTimeOnly = TimeOnly.FromDateTime(currentDate);

                switch (TypeVehicle.Name)
                {                    
                    case Enums.ConstValues.Cr:
                        if (disability == Enums.DisabilityValues.SI.ToString())
                        {
                            //ParkingLot.CantSpacesCar += (int)Enums.NumbersValues.b;
                            if (ParkingLot.CantSpacesDisability <= (int)Enums.NumbersValues.a)
                            {
                                //return new ServiceResponse()
                                //{
                                //    Result = ServiceResponseType.Failed,
                                //    ErrorMessage = "there are no available spaces"
                                //};
                                test = (int)Enums.NumbersValues.a;
                            }
                            break;
                        }
                        if (ParkingLot.CantSpacesCar <= (int)Enums.NumbersValues.a)
                        {
                            //return new ServiceResponse()
                            //{
                            //    Result = ServiceResponseType.Failed,
                            //    ErrorMessage = "there are no available spaces"
                            //};
                            test = (int)Enums.NumbersValues.a;
                        }
                        break;
                    case Enums.ConstValues.Mt:
                        if (ParkingLot.CantSpacesMotorcycle <= (int)Enums.NumbersValues.a)
                        {
                            //return new ServiceResponse()
                            //{
                            //    Result = ServiceResponseType.Failed,
                            //    ErrorMessage = "there are no available spaces"
                            //};
                            test = (int)Enums.NumbersValues.a;
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognized vehicle type");
                        break;
                }

                //Validation so that the date stated is greater than the present one
                if (requestedDateOnly < CurrentDateOnly)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Select a future date"
                    };
                }
                //This validation is to not allow registrations at hours less than this
                if (starttime <= CurrentTimeOnly)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Select a future hour"
                    };
                }

                //Validation so that the start time is not greater than the end time
                if (starttime >= endtime)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Hours not allowed"
                    };
                }

                var Reservations = await _context.reservations.Where(x => x.ParkingLotId == parkingLotId && x.Date == date).ToListAsync();

                if (!ValidateReservation(Reservations, starttime, endtime, test))
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Time span not available"
                    };
                }

                await _context.reservations.AddAsync(new Reservation()
                {
                    Id = Guid.NewGuid(),
                    //Date = TimeZoneInfo.ConvertTime(date, colombiaTimeZone),
                    Date = date.ToString(),
                    StartTime = starttime.ToString(),
                    EndTime = endtime.ToString(),
                    TypeVehicleId = typeVehicleId,
                    ParkingLotId = parkingLotId,
                    Disabled = disability,
                    TotalPrice = totalprice,
                    //ClientParkingLotId = Guid.Parse("f70cf415-4647-4de2-9b8a-5cfffad8b090") // Carlos
                    ClientParkingLotId = Guid.Parse("2BAA7CF8-B6E3-4FE1-9316-8E294507C5D4") // Jorge

                });

                // This switch is to subtract the number of spaces depending on the reservation made
                switch (TypeVehicle.Name)
                {
                    case Enums.ConstValues.Cr:
                        if (disability == Enums.DisabilityValues.SI.ToString())
                        {
                            //ParkingLot.CantSpacesCar += (int)Enums.NumbersValues.b;
                            ParkingLot.CantSpacesDisability -= (int)Enums.NumbersValues.b;
                            break;
                        }
                        ParkingLot.CantSpacesCar -= (int)Enums.NumbersValues.b;
                        break;
                    case Enums.ConstValues.Mt:
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

        public async Task<IEnumerable<Reservation>> GetReservationsParkingLot(Guid parkingLotId)
        {
            return await _context.reservations.Include(x => x.ClientParkingLot).Where(x => x.ParkingLotId == parkingLotId).ToListAsync();
        }
        
        public async Task<ServiceResponse> UpdateReservation(Guid ReservationId, string date, string startTime, string endTime, long totalPrice, string disabled)
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

        public bool ValidateReservation(IEnumerable<Reservation> reservations, TimeOnly starttime, TimeOnly endtime, int test)
        {

            if (reservations.Count() == (int)Enums.NumbersValues.a)
            {
                return true;
            }

            if (test == (int)Enums.NumbersValues.b)
            {
                return true;
            }

            foreach (var Reservation in reservations)
            {
                var StartT = TimeOnly.Parse(Reservation.StartTime);
                var EndT = TimeOnly.Parse(Reservation.EndTime);

                var V1 = starttime >= StartT; // si
                var V2 = endtime >= StartT;   // si    true

                var V3 = endtime <= EndT;    //no
                var V4 = starttime <= EndT;   //no    true

                if (V1 == V2 && V3 == V4 && (V1 != V3 && V1 != V4) && (V2 != V3 && V2 != V4))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
