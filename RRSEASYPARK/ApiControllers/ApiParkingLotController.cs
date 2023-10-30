using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Service;
using System.Security.Claims;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        private readonly IMapper _mapper;

        public ApiParkingLotController(IParkingLotService parkingLotService, IMapper mapper)
        {
            _parkingLotService = parkingLotService;
            _mapper = mapper;
        }
        /// <summary>
        /// This API method is where we get all the parking lots registered in our database.
        /// </summary>
        /// <returns>A list of parking lots.</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Client")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ParkingLotDto>), 200)]
        public async Task<IEnumerable<ParkingLotDto>> GetParkingLots()
        {
            var Parks = await _parkingLotService.GetParkingLots();
            var ParkingLotList = _mapper.Map<List<ParkingLot>, List<ParkingLotDto>>(Parks.ToList());           
            return ParkingLotList;
        }

        /// <summary>
        /// This API method is where we get all the parking lots registered in our database depending on a parking lot owner.
        /// </summary>
        /// <returns>A list of parking lots.</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Propietary Park")]
        [HttpGet("propietario")]
        [ProducesResponseType(typeof(IEnumerable<ParkingLotDto>), 200)]
        public async Task<IEnumerable<ParkingLotDto>> GetParkingLotsPropietary()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Parks = await _parkingLotService.GetParkingLotsPropietary(Guid.Parse(user));
            var ParkingLotList = _mapper.Map<List<ParkingLot>, List<ParkingLotDto>>(Parks.ToList());
            return ParkingLotList;
        }

        /// <summary>
        /// This API method is used to add a parking lot to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Client,Propietary Park")]
        [HttpGet("obtener/{id}")]
        [ProducesResponseType(typeof(ParkingLotDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ParkingLotDto> GetParkingLot(Guid id)
        {
            var ParkingLotOne = await _parkingLotService.GetParkingLot(id);
            var ParkingLotOneId = _mapper.Map<ParkingLot, ParkingLotDto>(ParkingLotOne);
            return ParkingLotOneId;
        }

        /// <summary>
        /// This API method is used to add a parking lot to the database
        /// </summary>
        /// <param name="parkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Propietary Park")]
        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddParkingLot(ParkingLotPostDto parkingLotDto)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _parkingLotService.AddParkingLot(parkingLotDto.Name, parkingLotDto.Adress, parkingLotDto.Nit, parkingLotDto.Telephone, parkingLotDto.NormalPrice, parkingLotDto.DisabilityPrice, parkingLotDto.Info, parkingLotDto.CantSpacesMotorcycle, parkingLotDto.CantSpacesCar, parkingLotDto.CantSpacesDisability,parkingLotDto.disabilityservices, parkingLotDto.Image, parkingLotDto.CityId, Guid.Parse(user));
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to update a parking lot in the database
        /// </summary>
        /// <param name="parkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateParkingLot(ParkingUpdateDto parkingLotDto)
        {
            var result = await _parkingLotService.UpdateParkingLot(parkingLotDto.Id, parkingLotDto.Name, parkingLotDto.Adress, parkingLotDto.Nit, parkingLotDto.Telephone, parkingLotDto.NormalPrice, parkingLotDto.DisabilityPrice, parkingLotDto.Info, parkingLotDto.CantSpacesMotorcycle, parkingLotDto.CantSpacesCar, parkingLotDto.CantSpacesDisability, parkingLotDto.disabilityservices, parkingLotDto.Image, parkingLotDto.CityId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to delete a parking lot in the database
        /// </summary>
        /// <param name="parkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteParkingLot(ParkingLotDto parkingLotDto)
        {
            var result = await _parkingLotService.DeleteParkingLot(parkingLotDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
