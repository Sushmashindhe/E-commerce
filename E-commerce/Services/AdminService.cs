using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace E_commerce.Services
{
    
    public class AdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }
 
        public async Task<string> AdminLogin(string useremail, string password)
        {
            if (useremail == "admin@gmail.com" && password == "admin")
            {
                return "Login successful";
            }
            else
            {
                return "Invalid email or password";
            }
        }
    }   
}

