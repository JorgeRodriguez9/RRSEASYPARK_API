using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        /// <summary>
        /// This API method is where we get all the reservations registered in our database.
        /// </summary>
        /// <returns>A list of reservations</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Propietary Park")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), 200)]
        public async Task<IEnumerable<ReservationDto>> GetReservations()
        {
            var Reservations = await _reservationService.GetReservations();
            var ReservationsList = _mapper.Map<List<Reservation>, List<ReservationDto>>(Reservations.ToList());
            return ReservationsList;
        }

        /// <summary>
        /// This API method is where we get all the reservations registered in our database depending on the chosen parking lot.
        /// </summary>
        /// <returns>A list of reservations</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Propietary Park")]
        [HttpGet("parkinglot/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), 200)]
        public async Task<IEnumerable<ReservationDto>> GetReservationsParkingLot(Guid id)
        {
            var Reservations = await _reservationService.GetReservationsParkingLot(id);
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

        /// <summary>
        /// This API method is used to add a reservation to the database
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        /// 
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddReservation(ReservationPostDto reservationPostDto)
        {
            var Client = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ParkingLotSelect = await _parkingLotService.GetParkingLot(reservationPostDto.ParkingLotId);
            var Price = (int)Enums.NumbersValues.a;
            if (reservationPostDto.Disability == Enums.DisabilityValues.SI.ToString())
            {
                Price = ParkingLotSelect.DisabilityPrice;
            }
            else
            {
                Price = ParkingLotSelect.NormalPrice;
            }
            var result = await _reservationService.AddReservation(reservationPostDto.Date, Price,reservationPostDto.starttime, reservationPostDto.Endtime, reservationPostDto.VehicleType, reservationPostDto.ParkingLotId, reservationPostDto.Disability);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to update a reservation in the database
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        /// 
        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.UpdateReservation(reservationDto.Id, reservationDto.Date, reservationDto.StartTime, reservationDto.EndTime, reservationDto.TotalPrice, reservationDto.Disabled);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to delete a reservation in the database
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [AllowAnonymous]
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteReservation(ReservationDto reservationDto)
        {
            var result = await _reservationService.DeleteReservation(reservationDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}

