using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalCoder.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            User user;
            try
            {
                user = UsersRepository.LoginWithUsername(username, password);
            }
            catch (ArgumentException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            long createdId;
            try
            {
                createdId = UsersRepository.Insert(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            user.ID = createdId;
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User value)
        {
            int result;
            try
            {
                result = UsersRepository.Update(value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok($"{result} registro(s) modificados.");
        }
    }
}
