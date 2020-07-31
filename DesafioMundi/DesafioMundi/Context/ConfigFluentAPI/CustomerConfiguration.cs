using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMundi.Context.ConfigFluentAPI
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder.HasMany(x => x.Charges)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
        }
    }
}
