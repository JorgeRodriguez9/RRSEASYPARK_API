using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;
        private readonly IParkingLotService _parkingLotService;
        private readonly IMapper _mapper;

        public ApiReservationController(IReservationService reservationService,  IMapper mapper, IParkingLotService parkingLotService)
        {
            _reservationService = reservationService;
            _mapper = mapper;
            _parkingLotService = parkingLotService;
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
        public async Task<IActionResult> AddReservation(ReservationPostDto reservationPostDto)
        {
            
            var ParkingLotSelect = await _parkingLotService.GetParkingLot(reservationPostDto.ParkingLotId);
            var Price = 0;
            if (reservationPostDto.Disability == Enums.DisabilityValues.SI.ToString())
            {
                Price = ParkingLotSelect.DisabilityPrice;
            }
            else
            {
                Price = ParkingLotSelect.NormalPrice;
            }
            var result = await _reservationService.AddReservation(reservationPostDto.StartDate, Price, reservationPostDto.EndDate, reservationPostDto.VehicleType, reservationPostDto.ParkingLotId, reservationPostDto.Disability);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.UpdateReservation(reservationDto.Id, reservationDto.StartDate, reservationDto.EndDate, reservationDto.TotalPrice, reservationDto.Disabled);
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

