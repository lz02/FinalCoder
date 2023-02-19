using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalCoder.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products;
            try
            {
                products = ProductsRepository.GetAll();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(products);
        }

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
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }

            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            long createdId;
            try
            {
                createdId = ProductsRepository.Insert(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok($"ID del producto: {createdId}.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Product updateModel)
        {
            if (ProductsRepository.GetById(id) == null)
            {
                return NotFound($"El producto con ID \"{id}\" no existe.");
            }

            int result;
            try
            {
                result = ProductsRepository.Update(id, updateModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok($"{result} registro(s) modificados.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Product product = ProductsRepository.GetById(id);
            if (product == null)
            {
                return NotFound($"El producto con ID \"{id}\" no existe.");
            }

            int result = ProductsRepository.Delete(product);
            return Ok($"{result} registro(s) modificados.");
        }
    }
}
