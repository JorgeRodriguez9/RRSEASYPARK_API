using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEASYPARK.Models.Dto;
using RRSEasyPark.Models;
using RRSEASYPARK.Service;
using Microsoft.AspNetCore.Authorization;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRolController : ControllerBase
    {

        private readonly IRolService _rolService;
        private readonly IMapper _mapper;

        public ApiRolController(IRolService rolService, IMapper mapper)
        {
            _rolService = rolService;
            _mapper = mapper;
        }
        /// <summary>
        /// This API method is where we get all the rols registered in our database.
        /// </summary>
        /// <returns>A list of rols.</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        public async Task<IEnumerable<RoleDto>> GetRols()
        {
            var rols = await _rolService.GetRols();
            var roleList = _mapper.Map<List<Rol>, List<RoleDto>>(rols.ToList());
            return roleList;
        }

        /// <summary>
        /// This API method is used to get the rol to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [Authorize(Roles = "Client,Propietary Park")]
        [HttpGet("obtener/{id}")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<RoleDto> GetRol(Guid id)
        {
            var roleOne = await _rolService.GetRol(id);
            var roleOneId = _mapper.Map<Rol, RoleDto>(roleOne);
            return roleOneId;
        }

    }
}
