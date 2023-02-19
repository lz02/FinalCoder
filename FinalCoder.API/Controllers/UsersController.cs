using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalCoder.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
                return BadRequest(e.Message);
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
            catch (ArgumentException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            user.ID = createdId;
            return Ok(user);
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            User user;
            try
            {
                user = UsersRepository.GetByUsername(username);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (user == null)
            {
                return NotFound($"El usuario con nombre \"{username}\" no existe.");
            }
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
                return BadRequest(e.Message);
            }

            return Ok($"{result} registro(s) modificados.");
        }
    }
}
