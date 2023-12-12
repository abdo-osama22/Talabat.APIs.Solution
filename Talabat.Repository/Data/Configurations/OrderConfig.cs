using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, Address => Address.WithOwner());

            builder.Property(O => O.status)
                .HasConversion(
                    OStatus => OStatus.ToString(),
                    OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );

            builder.Property(O => O.Subtotal)
               .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DeliveryMethod).WithMany()
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
