using EasyShop.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


   //Configure DbContext
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    string cs="Server=(localdb)\\MSSQLLocalDB;Database=ShoppingCart;Integrated Security=True;TrustServerCertificate=True;";
     optionsBuilder.UseSqlServer(cs, options =>
    {
        options.CommandTimeout(120); // Set command timeout to 120 seconds (2 minutes)
    });
  }
  public DbSet<Customer> Customers{get;set;}
  public DbSet<Category> Categories{get;set;}
  public DbSet<Product> Products{get;set;}
  public DbSet<Cart> Carts{get;set;}
  public DbSet<Order> Orders{get;set;}
  public DbSet<OrderItem> OrderItems{get;set;}
  public DbSet<DeliveryDetail> DeliveryDetails{get;set;}
  public DbSet<Transaction> Transactions{get;set;}
}
