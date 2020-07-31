using DesafioMundi.Context.ConfigFluentAPI;
using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioMundi.Context
{
    public class MundiContext : DbContext
    {

        public MundiContext(DbContextOptions<MundiContext> options):base(options)
        {}

        public DbSet<Customer> Customers { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<CreditCard> CreditCards  { get; set; }
        public DbSet<Item> Items { get; set; }
        

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;
            //"Data Source=MI-SEU;Initial Catalog=DbMundi;Integrated Security=True"
            //"Data Source=MI-SEU\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChargeConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());


        }

    }
}
