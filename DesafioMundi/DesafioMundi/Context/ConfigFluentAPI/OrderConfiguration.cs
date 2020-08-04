using DesafioMundi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMundi.Context.ConfigFluentAPI
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(x => x.Items)
                    .WithOne(x => x.Order)
                    .HasForeignKey(x => x.OrderId);
            //builder.HasOne(x => x.Charge)
            //        .WithOne(x => x.Order)
            //        .HasForeignKey<Order>(x => x.ChargeId);
        }
    }
}