using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCart(Cart cart)
        {
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCart(int userId)
        {
            return await _context.Cart
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task DeleteCartItem(int id)
        {
            var item = await _context.Cart.FindAsync(id);

            if (item != null)
            {
                _context.Cart.Remove(item);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<object> Checkout(int userId)
        {
            var cartItems = await _context.Cart
                .Where(x => x.UserId == userId)
                .ToListAsync();

            decimal totalAmount = 0;

            foreach (var item in cartItems)
            {
                var product = await _context.Product
                    .FindAsync(item.ProductId);

                if (product != null)
                {
                    totalAmount +=
                        (product.Price ?? 0)
                        * item.Quantity;
                }
            }
            foreach (var item in cartItems)
            {
                var product =
                    await _context.Product
                    .FindAsync(item.ProductId);

                if (product != null)
                {
                    var order = new Order
                    {
                        UserId = userId,

                        ProductName = product.Name,

                        Price = product.Price ?? 0,

                        Quantity = item.Quantity,

                        TotalAmount =
                            (product.Price ?? 0)
                            * item.Quantity,

                        OrderDate = DateTime.Now
                    };

                    _context.Order.Add(order);
                }
            }
            _context.Cart.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return new
            {
                TotalAmount = totalAmount,
                BillDate = DateTime.Now
            };
        }
        public async Task IncreaseQuantity(int id)
        {
            var item =
                await _context.Cart.FindAsync(id);

            if (item != null)
            {
                item.Quantity++;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DecreaseQuantity(int id)
        {
            var item =
                await _context.Cart.FindAsync(id);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;

                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Order>>GetOrders(int userId)
        {
            return await _context.Order
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }
    }
}