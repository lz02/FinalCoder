using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalCoder.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] string query)
        {
            IEnumerable<Product> products;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    products = ProductsRepository.GetAll();
                }
                else
                {
                    products = ProductsRepository.Search(query);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Product product;
            try
            {
                product = ProductsRepository.GetById(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(product);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetByUser(long id)
        {
            IEnumerable<Product> products;
            try
            {
                products = ProductsRepository.GetAllByUser(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(products);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            int result;
            try
            {
                result = ProductsRepository.Insert(value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok($"{result} registro(s) modificados.");
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Product value)
        {
            int result;
            try
            {
                result = ProductsRepository.Update(value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok($"{result} registro(s) modificados.");
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Product product;
            try
            {
                product = ProductsRepository.GetById(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            int result = ProductsRepository.Delete(product);
            return Ok($"{result} registro(s) modificados.");
        }
    }
}
