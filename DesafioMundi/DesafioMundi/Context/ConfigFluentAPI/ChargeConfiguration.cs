using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace DesafioMundi.Context.ConfigFluentAPI
{
    public class ChargeConfiguration : IEntityTypeConfiguration<Charge>
    {
        public void Configure(EntityTypeBuilder<Charge> builder)
        { 
            //builder.HasOne(x => x.CreditCard)
            //       .WithMany(y => y.Charges)
            //       .HasForeignKey(z => z.CreditCardId);
            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Charges)
                   .HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.Order)
                   .WithOne(x => x.Charge)
                   .HasForeignKey<Charge>(d => d.OrderId);                  

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever(); 
        }
    }
}
