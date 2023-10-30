using Microsoft.EntityFrameworkCore;
using RRSEasyPark.Models;
using RRSEASYPARK.DAL;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;

namespace RRSEASYPARK.Service
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly RRSEASYPARKContext _context;

        public ParkingLotService(RRSEASYPARKContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingLot>> GetParkingLots()
        {
            return await _context.parkingLots.Include(x => x.City).ToListAsync();
        }

        public async Task<IEnumerable<ParkingLot>> GetParkingLotsPropietary(Guid user)
        {
            var propietary = await _context.propietaryParks.FirstOrDefaultAsync(x => x.UserId == user);
            return await _context.parkingLots.Where(x => x.PropietaryParkId == propietary.Id).ToListAsync();
        }
        public async Task<ParkingLot?> GetParkingLot(Guid parkingLotId)
        {
            return await _context.parkingLots.Include(x => x.City).FirstOrDefaultAsync(x => x.Id == parkingLotId); ;
        }

        public async Task<ServiceResponse> AddParkingLot(string name, string adress, string nit, long telefhone, int price, int disabilityPrice, string info, int cantSpacesMoto, int cantSpacesCar, int cantSpacesDisability, string disabilityservices, string image, Guid cityId, Guid user)
        {
            try
            {
                var propietary = await _context.propietaryParks.FirstOrDefaultAsync(x => x.UserId == user);
                var propietaryGuid = propietary.Id;

                await _context.parkingLots.AddAsync(new ParkingLot()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Adress = adress,
                    Nit = nit,
                    Telephone = telefhone,
                    NormalPrice = price,
                    DisabilityPrice = disabilityPrice,
                    Info = info,
                    CantSpacesMotorcycle = cantSpacesMoto,
                    CantSpacesCar = cantSpacesCar,
                    CantSpacesDisability = cantSpacesDisability,
                    disabilityservices = disabilityservices,
                    Image = image,
                    CityId = cityId,
                    PropietaryParkId = propietaryGuid

                });
                await _context.SaveChangesAsync();

                return new ServiceResponse()
                {
                    Result = ServiceResponseType.Succeded,
                    InformationMessage = "parking lot add Correct"
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

        public async Task<ServiceResponse> UpdateParkingLot(Guid parkingLotId, string name, string adress, string nit, long telefhone, int price, int disabilityPrice, string info, int cantSpacesMoto, int cantSpacesCar, int cantSpacesDisability, string disabilityservices, string image, Guid cityId)
        {
            try
            {
                var parkingLot = await _context.parkingLots.FindAsync(parkingLotId);
                if (parkingLot == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Parking lot don't exist"
                    };
                }
                parkingLot.Name = name;
                parkingLot.Adress = adress;
                parkingLot.Nit = nit;
                parkingLot.Telephone = telefhone;
                parkingLot.NormalPrice = price;
                parkingLot.DisabilityPrice = disabilityPrice;
                parkingLot.Image = image;
                parkingLot.Info = info;
                parkingLot.CantSpacesMotorcycle = cantSpacesMoto;
                parkingLot.CantSpacesCar = cantSpacesCar;
                parkingLot.CantSpacesDisability = cantSpacesDisability;
                parkingLot.disabilityservices = disabilityservices;
                parkingLot.CityId = cityId;

                _context.parkingLots.Update(parkingLot);

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
        public async Task<ServiceResponse?> DeleteParkingLot(Guid parkingLotId)
        {
            try
            {
                var parkingLot = await _context.parkingLots.FindAsync(parkingLotId);

                if (parkingLot == null)
                {
                    return new ServiceResponse()
                    {
                        Result = ServiceResponseType.Failed,
                        ErrorMessage = "Parking lot don't exist"
                    };
                }
                _context.parkingLots.Remove(parkingLot);
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
