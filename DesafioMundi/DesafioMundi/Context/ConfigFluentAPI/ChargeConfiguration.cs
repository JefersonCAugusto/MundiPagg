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
          

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Charges);
            builder.HasOne(x => x.Order)
                   .WithOne(x => x.Charge);
              builder.HasKey(x => new { x.Id, x.CustomerId, x.OrderId });
             
        }
    }
}
