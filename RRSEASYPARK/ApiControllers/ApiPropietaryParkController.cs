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
    public class ApiPropietaryParkController : ControllerBase
    {

        private readonly IPropietaryParkService _propietaryParkService;
        private readonly IMapper _mapper;

        public ApiPropietaryParkController(IPropietaryParkService propietaryParkService, IMapper mapper)
        {
            _propietaryParkService = propietaryParkService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PropietaryParkDto>> GetPropietaryParks()
        {
            var PropietaryParks = await _propietaryParkService.GetPropietaryParks();
            var PropietaryParksList = _mapper.Map<List<PropietaryPark>, List<PropietaryParkDto>>(PropietaryParks.ToList());
            return PropietaryParksList;
        }

        [HttpPost]
        public async Task<IActionResult> AddPropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.AddPropietaryPark(propietaryParkDto.Name, propietaryParkDto.Identification, propietaryParkDto.Email, propietaryParkDto.UserId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.UpdatePropietaryPark(propietaryParkDto.Id, propietaryParkDto.Name, propietaryParkDto.Identification, propietaryParkDto.Email);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePropietaryPark(PropietaryParkDto propietaryParkDto)
        {
            var result = await _propietaryParkService.DeletePropietaryPark(propietaryParkDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
