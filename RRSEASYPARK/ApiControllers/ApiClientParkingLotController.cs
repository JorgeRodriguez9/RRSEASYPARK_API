using AutoMapper;
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

        [HttpGet]
        public async Task<IEnumerable<ClientParkingLotDto>> GetClients()
        {
            var Clients = await _clientParkingLotService.GetClientParkingLots();
            var ClientsList = _mapper.Map<List<ClientParkingLot>, List<ClientParkingLotDto>>(Clients.ToList());
            return ClientsList;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.AddClientParkingLot(clientParkingLotDto.Name, clientParkingLotDto.Identification, clientParkingLotDto.Email, clientParkingLotDto.Telephone, clientParkingLotDto.UserId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.UpdateClientParkingLot(clientParkingLotDto.Id, clientParkingLotDto.Name, clientParkingLotDto.Identification, clientParkingLotDto.Email, clientParkingLotDto.Telephone);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(ClientParkingLotDto clientParkingLotDto)
        {
            var result = await _clientParkingLotService.DeleteClientParkingLot(clientParkingLotDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
