using Microsoft.EntityFrameworkCore;
using OnlineMarket.Model;

namespace OnlineMarket.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }
    
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}