using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMundi.Context.ConfigFluentAPI
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasOne(x => x.Customer)
                    .WithMany(x => x.CreditCard)
                    .HasForeignKey(x => x.CustomerID);
            builder.HasKey(x => x.Id); 
            builder.Ignore(x => x.ExpMonth)
                   .Ignore(x => x.CVV)
                   .Ignore(x => x.ExpMonth)
                   .Ignore(x => x.ExpYear)
                   .Ignore(x => x.Number);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever(); 
            builder.Property(x => x.LestFourNumbers).IsRequired().HasMaxLength(4);
            builder.Property(x => x.Brand).IsRequired().HasMaxLength(15);




        }
 
    }
}
