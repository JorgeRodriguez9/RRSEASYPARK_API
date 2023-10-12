using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Service;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiClientParkingLotController : ControllerBase
    {
        private readonly IClientParkingLotService _clientParkingLotService;
        private readonly IMapper _mapper;

        public ApiClientParkingLotController(IClientParkingLotService clientParkingLotService, IMapper mapper)
        {
            _clientParkingLotService = clientParkingLotService;
            _mapper = mapper;
        }
        /// <summary>
        /// This API method is where we get all the parking customers registered in our database.
        /// </summary>
        /// <returns>A list of parking customers.</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientParkingLotDto>), 200)]
        public async Task<IEnumerable<ClientParkingLotDto>> GetClients()
        {
            var Clients = await _clientParkingLotService.GetClientParkingLots();
            var ClientsList = _mapper.Map<List<ClientParkingLot>, List<ClientParkingLotDto>>(Clients.ToList());
            return ClientsList;
        }
        /// <summary>
        /// This API method is used to add a parking client to the database
        /// </summary>
        /// <param name="clientParkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.AddClientParkingLot(clientParkingLotDto.Name, clientParkingLotDto.Identification, clientParkingLotDto.Email, clientParkingLotDto.Telephone, clientParkingLotDto.UserId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
        /// <summary>
        /// This API method is used to update a client in the database
        /// </summary>
        /// <param name="clientParkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.UpdateClientParkingLot(clientParkingLotDto.Id, clientParkingLotDto.Name, clientParkingLotDto.Identification, clientParkingLotDto.Email, clientParkingLotDto.Telephone);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
        /// <summary>
        /// This API method is used to delete a client in the database
        /// </summary>
        /// <param name="clientParkingLotDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [AllowAnonymous]
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.DeleteClientParkingLot(clientParkingLotDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
