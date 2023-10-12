using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Service;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiCityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public ApiCityController (ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        /// <summary>
        /// This API method is where we get all the city registered in our database.
        /// </summary>
        /// <returns>A list of citys</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Client,Propietary Park")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityDto>), 200)]
        public async Task<IEnumerable<CityDto>> GetCities()
        {
            var Cities = await _cityService.GetCities();
            var CitiesList = _mapper.Map<List<City>, List<CityDto>>(Cities.ToList());
            return CitiesList;
        }

    }
}
