using Microsoft.EntityFrameworkCore;

namespace Customer_Order_Management_App.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; } //Table Name
    }
}
