using AutoMapper;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using System.Runtime.InteropServices;

namespace RRSEASYPARK.Utilities
{
    public class AutoMaperProfile : Profile
    {
        public AutoMaperProfile()
        {
            CreateMap<City, CityDto>();

            CreateMap<ClientParkingLot, ClientParkingLotDto>();

            CreateMap<ParkingLot, ParkingLotDto>().ForMember(destiny => destiny.CityName,
            opt => opt.MapFrom(origen => origen.City.Name));

            CreateMap<PropietaryPark, PropietaryParkDto>();

            CreateMap<Reservation, ReservationDto>();

            CreateMap<User, UserDto>();

            CreateMap<TypeVehicle, TypeVehicleDto>();

        }
    }
}