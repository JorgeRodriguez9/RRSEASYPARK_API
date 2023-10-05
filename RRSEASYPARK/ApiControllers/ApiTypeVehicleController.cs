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

        /// <summary>
        /// This API method is where we get all the types vehicles registered in our database.
        /// </summary>
        /// <returns>A list of type vehicles</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TypeVehicleDto>), 200)]
        public async Task<IEnumerable<TypeVehicleDto>> GetTypeVehicles()
        {
            var Vehicles = await _typeVehicleService.GetTypeVehicles();
            var VehiclesList = _mapper.Map<List<TypeVehicle>, List<TypeVehicleDto>>(Vehicles.ToList());
            return VehiclesList;
        }


    }
}
