using E_commerce.Models;
using E_commerce.Services;
using E_CommerceTests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceTests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private AppDbContext _context;
        private UserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = TestDbContextFactory.Create();
            _userService = new UserService(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }
        [TestMethod]
        public async Task AddUser_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = new E_commerce.Models.User
            {
                Name = "John Doe",
                Email = "john.doe@gmail.com",
                Password = "123"
            };
            await _userService.AddUser(user);
            Assert.IsNotNull(user);

        }
        [TestMethod]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            await _context.User.AddRangeAsync(
                new User
                {
                    Name = "John",
                    Email = "john@gmail.com",
                    Password = "123"
                },
                new User
                {
                    Name = "Steve",
                    Email = "steve@gmail.com",
                    Password = "456"
                });

            await _context.SaveChangesAsync();

            var users = await _userService.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }
        [TestMethod]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var user = new User
            {
                Name = "John",
                Email = "john@gmail.com",
                Password = "123"
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userService.Login("john@gmail.com", "123");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Login_ShouldNotReturnUser_WhenCredentialsAreInvalid()
        {
            var user = new User
            {
                Name = "John",
                Email = "john@gmail.com",
                Password = "123"
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userService.Login("bob@gmail.com", "1234");
            Assert.IsNull(result);
        }
    }
}
