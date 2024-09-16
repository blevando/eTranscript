using eTranscript.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace eTranscript.Data
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Commodity> Commodity { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<PaymentGatewayConfig> PaymentGatewayConfig { get; set; }
        public DbSet<SplitConfig> SplitConfig { get; set; }

    }
}
