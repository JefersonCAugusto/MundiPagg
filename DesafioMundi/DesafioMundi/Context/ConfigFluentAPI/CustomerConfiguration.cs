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
            builder.HasMany(x => x.Charges)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(35);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Type).HasMaxLength(15);
            builder.Property(x => x.Document).HasMaxLength(20);
            builder.Property(x => x.Gender).HasMaxLength(10); 

        }
    }
}
