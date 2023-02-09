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
                return BadRequest(e.Message);
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

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
