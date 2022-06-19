using Async_Inn.Models.api;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterUser registerUser)
        {
            try
            {
                var user = await _userService.Register(registerUser, this.ModelState);
                if (ModelState.IsValid)
                {
                    return user;
                }   
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData loginData)
        {
            try
            {
                var user = await _userService.Authenticate(loginData.Username, loginData.Password);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
