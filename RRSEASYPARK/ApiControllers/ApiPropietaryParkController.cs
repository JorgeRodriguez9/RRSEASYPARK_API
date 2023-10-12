using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPropietaryParkController : ControllerBase
    {

        private readonly IPropietaryParkService _propietaryParkService;
        private readonly IMapper _mapper;

        public ApiPropietaryParkController(IPropietaryParkService propietaryParkService, IMapper mapper)
        {
            _propietaryParkService = propietaryParkService;
            _mapper = mapper;
        }

        /// <summary>
        /// This API method is where we get all the propietary parks registered in our database.
        /// </summary>
        /// <returns>A list of propietarys parking lots</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropietaryParkDto>), 200)]
        public async Task<IEnumerable<PropietaryParkDto>> GetPropietaryParks()
        {
            var PropietaryParks = await _propietaryParkService.GetPropietaryParks();
            var PropietaryParksList = _mapper.Map<List<PropietaryPark>, List<PropietaryParkDto>>(PropietaryParks.ToList());
            return PropietaryParksList;
        }

        /// <summary>
        /// This API method is used to add a propietary park to the database
        /// </summary>
        /// <param name="propietaryParkDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddPropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.AddPropietaryPark(propietaryParkDto.Name, propietaryParkDto.Identification, propietaryParkDto.Email, propietaryParkDto.UserId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to update a propietary park in the database
        /// </summary>
        /// <param name="propietaryParkDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdatePropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.UpdatePropietaryPark(propietaryParkDto.Id, propietaryParkDto.Name, propietaryParkDto.Identification, propietaryParkDto.Email);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to delete a propietary park in the database
        /// </summary>
        /// <param name="propietaryParkDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeletePropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.DeletePropietaryPark(propietaryParkDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
