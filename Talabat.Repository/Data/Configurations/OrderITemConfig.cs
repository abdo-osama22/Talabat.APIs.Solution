using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderITemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderItem => orderItem.Product, Product => Product.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(18,2)");    
        }
    }
}
