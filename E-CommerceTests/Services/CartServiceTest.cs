using E_commerce.Services;
using E_CommerceTests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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
        public async Task AddToCart_ShouldAddItemToCart()
        {
            // Arrange
            var userId = 1;
            var productId = 1;
            var quantity = 2;
            // Act
            await _cartService.AddToCart(userId, productId, quantity);
            // Assert
            var cartItems = await _cartService.GetCartItems(userId);
            Assert.AreEqual(1, cartItems.Count);
            Assert.AreEqual(productId, cartItems[0].ProductId);
            Assert.AreEqual(quantity, cartItems[0].Quantity);
        }
    }
}
