using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMundi.Context.ConfigFluentAPI
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        { 
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.ExpMonth)
                   .Ignore(x => x.CVV)
                   .Ignore(x => x.ExpMonth)
                   .Ignore(x => x.ExpYear)
                   .Ignore(x => x.Number);



            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever(); 
            builder.Property(x => x.LestFourNumbers).IsRequired();
            builder.Property(x => x.Brand).IsRequired();

        }
 
    }
}
