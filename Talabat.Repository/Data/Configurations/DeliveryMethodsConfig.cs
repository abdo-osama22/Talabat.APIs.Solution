using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Configurations
{
    internal class DeliveryMethodsConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
             builder.Property(deliveryMethods => deliveryMethods.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
