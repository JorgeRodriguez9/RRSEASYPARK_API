using AutoMapper;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models.Request;
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

            CreateMap<Rol, RoleDto>();

            CreateMap<Reservation, ReservationDto>().ForMember(destiny => destiny.ClientName,
            opt => opt.MapFrom(origen => origen.ClientParkingLot.Name)).ForMember(destiny => destiny.Telephone,
            opt => opt.MapFrom(origen => origen.ClientParkingLot.Telephone));

            CreateMap<User, UserDto>();

            CreateMap<User, UserRequest>().ForMember(destiny => destiny.RolName, opt => opt.MapFrom(origen => origen.Rol.Name));

            CreateMap<TypeVehicle, TypeVehicleDto>();

        }
    }
}