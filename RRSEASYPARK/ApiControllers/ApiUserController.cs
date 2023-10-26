using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRSEasyPark.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using AutoMapper;
using RRSEASYPARK.Models.Request;
using Azure;

namespace RRSEASYPARK.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ApiUserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// his API method is where we get all the users registered in our database.
        /// </summary>
        /// <returns>A list of users</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IEnumerable<UserDto>> GetUser()
        {
            var Users = await _userService.GetUser();
            var UsersList = _mapper.Map<List<User>, List<UserDto>>(Users.ToList());
            return UsersList;
        }


        [HttpPost("Login")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Autentification([FromBody] AuthRequest user)
        {

            ServiceResponse response = new ServiceResponse();
            try
            {
              
                var userresponse = _userService.Auth(user);

                if (userresponse == null)
                {
                    response.Result = ServiceResponseType.Failed;
                    response.ErrorMessage = "Usuario o contraseña incorrecta";
                    return BadRequest();
                }

                response.Result = ServiceResponseType.Succeded;
                response.Data = userresponse;

                return Ok(response);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
           
        }

        /// <summary>
        /// This API method is used to update a user in the database
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var result = await _userService.UpdateUser(userDto.Id, userDto.Name, userDto.Password);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// This API method is used to delete a reservation in the database
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>A response code</returns>
        /// <response code= "200">Customers have been obtained correctly</response>
        /// <response code= "400">The server cannot satisfy a request</response>
        /// <response code= "500">Database connection failure</response>
        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteUser(UserDto userDto)
        {
            var result = await _userService.DeleteUser(userDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
