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
    public class ApiParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        private readonly IMapper _mapper;

        public ApiParkingLotController(IParkingLotService parkingLotService, IMapper mapper)
        {
            _parkingLotService = parkingLotService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ParkingLotDto>> GetParkingLots()
        {
            var Parks = await _parkingLotService.GetParkingLots();
            var ParkingLotList = _mapper.Map<List<ParkingLot>, List<ParkingLotDto>>(Parks.ToList());           
            return ParkingLotList;
        }

        [HttpGet("obtener/{id}")]
        public async Task<ParkingLotDto> GetParkingLot(Guid id)
        {
            var ParkingLotOne = await _parkingLotService.GetParkingLot(id);
            var ParkingLotOneId = _mapper.Map<ParkingLot, ParkingLotDto>(ParkingLotOne);
            return ParkingLotOneId;
        }

        [HttpPost]
        public async Task<IActionResult> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            var result = await _parkingLotService.AddParkingLot(parkingLotDto.Name, parkingLotDto.Adress, parkingLotDto.Nit, parkingLotDto.Telephone, parkingLotDto.NormalPrice, parkingLotDto.DisabilityPrice, parkingLotDto.Info, parkingLotDto.CantSpacesMotorcycle, parkingLotDto.CantSpacesCar, parkingLotDto.CantSpacesDisability,parkingLotDto.CityId, parkingLotDto.PropietaryParkId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPost("imagenes")]
        public async Task<IActionResult> Images([FromBody]string Images)
        {
            var result = await _parkingLotService.AddImages(Images);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateParkingLot(ParkingLotDto parkingLotDto)
        {
            var result = await _parkingLotService.UpdateParkingLot(parkingLotDto.Id, parkingLotDto.Name, parkingLotDto.Adress, parkingLotDto.Nit, parkingLotDto.Telephone, parkingLotDto.NormalPrice, parkingLotDto.DisabilityPrice, parkingLotDto.Info, parkingLotDto.CantSpacesMotorcycle, parkingLotDto.CantSpacesCar, parkingLotDto.CantSpacesDisability);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParkingLot(ParkingLotDto parkingLotDto)
        {
            var result = await _parkingLotService.DeleteParkingLot(parkingLotDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
