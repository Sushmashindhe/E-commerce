using E_commerce.Services;
using E_CommerceTests.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceTests.Services
{
    [TestClass]
    public class AdminServiceTest
    {
        private AppDbContext _context;
        private AdminService _adminService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = TestDbContextFactory.Create();
            _adminService = new AdminService(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
        [TestMethod]

        public async Task AdminLogin_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            string email = "admin@gmail.com";
            string password = "admin";

            // Act
            string result = await _adminService.AdminLogin(email,password);

            // Assert
            Assert.AreEqual("Login successful",result);
        }

        [TestMethod]
        public async Task AdminLogin_InvalidCredentials_ReturnsError()
        {
            // Arrange
            string email = "test@gmail.com";
            string password = "123";

            // Act
            string result = await _adminService.AdminLogin(email, password);

            // Assert
            Assert.AreEqual("Invalid email or password",result);
        }

    }

}
