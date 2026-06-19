using E_commerce.Models;
using E_commerce.Services;
using E_CommerceTests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceTests.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private AppDbContext _context;
        private ProductService _productService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = TestDbContextFactory.Create();
            _productService = new ProductService(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
        [TestMethod]

        public async Task GetAllProducts_ReturnsAllProducts()
        {
            // Arrange
            var product1 = new Product
            {
                Name = "Product 1",
                Description = "Description 1",
                Price = 10.0m,
                Category = E_commerce.Enum.Category.Clothing,
                ImageUrl = "image1.jpg"
            };
            var product2 = new Product
            {
                Name = "Product 2",
                Description = "Description 2",
                Price = 20.0m,
                Category = E_commerce.Enum.Category.Electronics,
                ImageUrl = "image2.jpg"
            };
            _context.Product.AddRange(product1, product2);
            await _context.SaveChangesAsync();
            // Act
            var products = await _productService.GetAllProducts();
            // Assert
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual("Product 1", products[0].Name);
            Assert.AreEqual("Product 2", products[1].Name);
        }
        [TestMethod]
        public async Task AddProduct_ShouldAddProductToDatabase()
        {
            // Arrange
            var product = new Product
            {
                Name = "New Product",
                Description = "New Description",
                Price = 30.0m,
                Category = E_commerce.Enum.Category.Books,
                ImageUrl = "image3.jpg"
            };
            // Act
            await _productService.AddProduct(product);
            // Assert
            var addedProduct = await _context.Product.FindAsync(product.Id);
            Assert.IsNotNull(addedProduct);
            Assert.AreEqual("New Product", addedProduct.Name);
        }
        [TestMethod]
        public async Task DeleteProduct_ShouldDeleteProduct()
        {
            var product = new Product
            {
                Name = "Laptop",
                Price = 50000
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var result = await _productService.DeleteProduct(product.Id);

            Assert.AreEqual("Product deleted successfully", result);
        }
        [TestMethod]
        public async Task DeleteProduct_ShouldNotDeleteProduct()
        {
           
            var result = await _productService.DeleteProduct(100);

            Assert.AreEqual("Product not found", result);
        }
        [TestMethod]
        public async Task UpdateProduct_ReturnTheUpdatedProduct()
        {
            var product = new Product
            {
                Name = "Laptop",
                Description = "Old",
                Price = 3000,
                Category = E_commerce.Enum.Category.Electronics,
                ImageUrl = "image3.jpg"
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            var updatedProduct = new Product
            {
                Name = "Laptop",
                Description = "New",
                Price = 75000,
                Category = E_commerce.Enum.Category.Electronics,
                ImageUrl = "image.jpg"
            };

            await _productService.UpdateProduct(product.Id, updatedProduct);

            var result = await _context.Product.FindAsync(product.Id);

            Assert.AreEqual("Laptop", result.Name);

            Assert.AreEqual(75000, result.Price);
        }
    }
}

