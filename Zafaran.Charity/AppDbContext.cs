using Microsoft.EntityFrameworkCore;
using Zafaran.Charity.Models;

namespace Zafaran.Charity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<SupportMessage> Messages{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Charity> Charities { get; set; }
        public DbSet<TheNews> News{ get; set; }
        public DbSet<Supporter>  Supporters{ get; set; }
    }
}