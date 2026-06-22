using E_commerce.Models;
using E_commerce.Services;
using E_CommerceTests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace E_CommerceTests.Services
{
    [TestClass]
    public class CartServiceTest
    {
        private AppDbContext _context;
        private CartService _cartService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = TestDbContextFactory.Create();
            _cartService = new CartService(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
        [TestMethod]
        public async Task AddToCart_ShouldAddItem()
        {
            var cart = new Cart
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 2
            };

            await _cartService.AddToCart(cart);

            var result = await _context.Cart.FirstOrDefaultAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [TestMethod]
        public async Task GetCart_ShouldReturnUserCart()
        {
            await _context.Cart.AddRangeAsync(
                new Cart
                {
                    UserId = 1,
                    ProductId = 1,
                    Quantity = 1
                },
                new Cart
                {
                    UserId = 1,
                    ProductId = 2,
                    Quantity = 2
                });

            await _context.SaveChangesAsync();

            var result = await _cartService.GetCart(1);

            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public async Task DeleteCartItem_ShouldRemoveItem()
        {
            var cart = new Cart
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 1
            };

            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            await _cartService.DeleteCartItem(cart.Id);

            Assert.AreEqual(0, await _context.Cart.CountAsync());
        }
        [TestMethod]
        public async Task IncreaseQuantity_ShouldIncreaseQuantity()
        {
            var cart = new Cart
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 1
            };

            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            await _cartService.IncreaseQuantity(cart.Id);

            var result = await _context.Cart.FindAsync(cart.Id);

            Assert.AreEqual(2, result.Quantity);
        }
        [TestMethod]
        public async Task DecreaseQuantity_ShouldDecreaseQuantity()
        {
            var cart = new Cart
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 3
            };

            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            await _cartService.DecreaseQuantity(cart.Id);

            var result = await _context.Cart.FindAsync(cart.Id);

            Assert.AreEqual(2, result.Quantity);
        }
        [TestMethod]
        public async Task DecreaseQuantity_ShouldNotGoBelowOne()
        {
            var cart = new Cart
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 1
            };

            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            await _cartService.DecreaseQuantity(cart.Id);

            var result = await _context.Cart.FindAsync(cart.Id);

            Assert.AreEqual(1, result.Quantity);
        }
        [TestMethod]
        public async Task GetOrders_ShouldReturnOrders()
        {
            await _context.Order.AddRangeAsync(
                new Order
                {
                    UserId = 1,
                    ProductName = "Laptop",
                    Price = 50000,
                    Quantity = 1,
                    TotalAmount = 50000
                },
                new Order
                {
                    UserId = 1,
                    ProductName = "Mobile",
                    Price = 20000,
                    Quantity = 1,
                    TotalAmount = 20000
                });

            await _context.SaveChangesAsync();

            var result =
                await _cartService.GetOrders(1);

            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public async Task Checkout_ShouldGenerateBill_And_CreateOrders()
        {
            var product = new Product
            {
                Name = "Laptop",
                Price = 50000
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var cart = new Cart
            {
                UserId = 1,
                ProductId = product.Id,

                Quantity = 2
            };

            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();

            var result =
                await _cartService.Checkout(1);

            Assert.AreEqual(
                0,
                await _context.Cart.CountAsync());

            Assert.AreEqual(
                1,
                await _context.Order.CountAsync());
        }
    }
}
