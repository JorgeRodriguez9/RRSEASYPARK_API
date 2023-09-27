using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using AutoMapper;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ApiReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationDto>> GetReservations()
        {
            var Reservations = await _reservationService.GetReservations();
            var ReservationsList = _mapper.Map<List<Reservation>, List<ReservationDto>>(Reservations.ToList());
            return ReservationsList;
        }

        //[HttpGet]
        //public async Task<ReservationDto> GetReservation(ReservationDto reservationDto)
        //{
        //    var Reservations = await _reservationService.GetReservation(reservationDto.Id);
        //    var ReservationsId = _mapper.Map<Reservation, ReservationDto>(Reservations);
        //    return ReservationsId;
        //}

        [HttpPost]
        public async Task<IActionResult> AddReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.AddReservation(reservationDto.Date, reservationDto.TotalPrice, reservationDto.Disabled, reservationDto.ClientId, reservationDto.TypeVehicleId, reservationDto.ParkingLotId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.UpdateReservation(reservationDto.Id, reservationDto.Date, reservationDto.TotalPrice, reservationDto.Disabled);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.DeleteReservation(reservationDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
