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
    public class ApiUserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ApiUserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUser()
        {
            var Users = await _userService.GetUser();
            var UsersList = _mapper.Map<List<User>, List<UserDto>>(Users.ToList());
            return UsersList;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            var result = await _userService.AddUser(userDto.Name, userDto.Password,userDto.RolId);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var result = await _userService.UpdateUser(userDto.Id, userDto.Name, userDto.Password);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserDto userDto)
        {
            var result = await _userService.DeleteUser(userDto.Id);
            return result.Result == ServiceResponseType.Succeded ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
