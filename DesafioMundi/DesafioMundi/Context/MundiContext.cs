using DesafioMundi.Context.ConfigFluentAPI;
using DesafioMundi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesafioMundi.Context
{
    public class MundiContext : IdentityDbContext
    {
        public MundiContext(DbContextOptions<MundiContext> options):base(options)
        {}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<CreditCard> CreditCards  { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ChargeConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }

    }
}
