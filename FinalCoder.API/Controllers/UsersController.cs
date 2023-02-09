using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            int result;
            try
            {
                result = UsersRepository.Insert(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(user);
        }

        // PUT api/<UserController>/5
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
