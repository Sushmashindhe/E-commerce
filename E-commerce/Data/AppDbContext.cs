using Microsoft.EntityFrameworkCore;
using E_commerce.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }

    public DbSet<Product> Product { get; set; }

    public DbSet<Cart> Cart { get; set; }

    public DbSet<Order> Order { get; set; }
}