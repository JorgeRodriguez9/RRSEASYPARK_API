using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEASYPARK.Models.Dto;
using RRSEasyPark.Models;
using RRSEASYPARK.Service;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTypeVehicleController : ControllerBase
    {

        private readonly ITypeVehiculeService _typeVehicleService;
        private readonly IMapper _mapper;

        public ApiTypeVehicleController(ITypeVehiculeService typeVehicleService, IMapper mapper)
        {
            _typeVehicleService = typeVehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TypeVehicleDto>> GetCities()
        {
            var Vehicles = await _typeVehicleService.GetTypeVehicles();
            var VehiclesList = _mapper.Map<List<TypeVehicle>, List<TypeVehicleDto>>(Vehicles.ToList());
            return VehiclesList;
        }


    }
}
