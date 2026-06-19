using E_commerce.Models;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
       private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product Name Required");

            if (product.Price <= 0)
                return BadRequest("Price must be greater than zero");

            await _productService.AddProduct(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product Name Required");

            if (product.Price == null || product.Price <= 0)
                return BadRequest("Price must be greater than zero");

            await _productService.UpdateProduct(id, product);

            return Ok();
        }
    }
}
