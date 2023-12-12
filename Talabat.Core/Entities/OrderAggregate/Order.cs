using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Order: BaseEntity
    {
        public Order()
        {

        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod,  decimal subtotal, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }

       // public int DeliveryMethodId { get; set; }//fk 

        public DeliveryMethod DeliveryMethod { get; set; } //Navigational Prop 1 

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();


        public decimal Subtotal { get; set; }
        //[NotMapped]
        //public decimal Total => Subtotal + DeliveryMethod.Cost;  //Derived Attribute
        public decimal GetTotal()
            => Subtotal + DeliveryMethod.Cost;


        public string PaymentItentId { get; set; } = string.Empty;

    }
}
