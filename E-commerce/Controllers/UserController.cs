using E_commerce.Models;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;


        public UserController(UserService userService, AppDbContext context)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var loginuser = await _userService.Login(user.Email, user.Password);
            if (loginuser == null)
            {
                return Unauthorized();
            }
            return Ok(loginuser);
        }
    }
}
