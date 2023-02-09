using FinalCoder.API.Models;
using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalCoder.API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Sale sale;
            try
            {
                sale = SalesRepository.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(sale);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAllByUser(long userId)
        {
            IEnumerable<Sale> sales;
            try
            {
                sales = SalesRepository.GetAllByUser(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(sales);
        }

        [HttpGet("{id}/items")]
        public IActionResult GetItemsOfSale(long id)
        {
            IEnumerable<ProductSale> productSales;
            try
            {
                productSales = ProductSalesRepository.GetAllItemsOfSale(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(productSales);
        }

        [HttpPost("{userId}")]
        public IActionResult Post(long userId, [FromBody] params SaleItemModel[] saleItems)
        {
            long saleId = SalesRepository.Insert(new Sale
            {
                Description = "",
                UserId = userId
            });

            foreach (SaleItemModel item in saleItems)
            {
                Product product = ProductsRepository.GetById(item.ProductId);
                if (product == null)
                {
                    return NotFound(item.ProductId);
                }
                if (product.Stock < item.Stock)
                {
                    return BadRequest($"No hay stock suficiente del producto {product.Description}");
                }

                ProductSale productSale = new ProductSale
                {
                    SaleId = saleId,
                    ProductId = item.ProductId,
                    Stock = item.Stock
                };

                product.Stock -= item.Stock;
                ProductsRepository.Update(product);
                ProductSalesRepository.Insert(productSale);
            }

            return Ok($"ID de la venta: {saleId}");
        }
    }
}
