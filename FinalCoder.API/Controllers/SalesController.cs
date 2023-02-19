using FinalCoder.API.Models;
using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

            if (sale == null)
            {
                return NotFound($"La venta con ID \"{id}\" no existe.");
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
            if (SalesRepository.GetById(id) == null)
            {
                return NotFound($"La venta con ID \"{id}\" no existe.");
            }

            IEnumerable<ProductSale> productSales;
            try
            {
                productSales = ProductSalesRepository.GetAllItemsOfSale(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (productSales.Count() == 0)
            {
                return Ok($"La venta con ID \"{id}\" no tiene items.");
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
                ProductsRepository.Update(item.ProductId, product);
                ProductSalesRepository.Insert(productSale);
            }

            return Ok($"ID de la venta: {saleId}");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Sale sale = SalesRepository.GetById(id);
            if (sale == null)
            {
                return NotFound($"La venta con ID \"{id}\" no se encontro.");
            }
            IEnumerable<ProductSale> saleItems = ProductSalesRepository.GetAllItemsOfSale(id);

            foreach (ProductSale item in saleItems)
            {
                Product product = ProductsRepository.GetById(item.ProductId);
                if (product == null)
                {
                    return NotFound(item.ProductId);
                }

                product.Stock += item.Stock;
                ProductsRepository.Update(item.ProductId, product);
                ProductSalesRepository.Delete(item);
            }

            int result = SalesRepository.Delete(sale);
            return Ok($"{result} registro(s) modificados.");
        }
    }
}
