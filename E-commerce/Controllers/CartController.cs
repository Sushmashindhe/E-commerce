using E_commerce.Models;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart cart)
        {
            await _cartService.AddToCart(cart);

            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCart(userId);

            return Ok(cart);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _cartService.DeleteCartItem(id);

            return Ok("Item Removed");
        }
        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var bill = await _cartService.Checkout(userId);

            return Ok(bill);
        }
        [HttpPut("increase/{id}")]

        public async Task<IActionResult>
        IncreaseQuantity(int id)
        {
            await _cartService
                .IncreaseQuantity(id);

            return Ok();
        }

        [HttpPut("decrease/{id}")] 

        public async Task<IActionResult>
        DecreaseQuantity(int id)
        {
            await _cartService
                .DecreaseQuantity(id);

            return Ok();
        }
        [HttpGet("orders/{userId}")]
        public async Task<IActionResult>GetOrders(int userId)
        {
            var orders =
                await _cartService
                .GetOrders(userId);

            return Ok(orders);
        }
    }
}