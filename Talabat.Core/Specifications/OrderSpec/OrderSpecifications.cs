using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Specifications.Specifications;

namespace Talabat.Core.Specifications.OrderSpec
{
    public class OrderSpecifications: BaseSpecifications<Order>
    {


       public OrderSpecifications(string Email):base(O=>O.BuyerEmail==Email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpecifications(string email, int orderId): base(o => o.BuyerEmail == email && o.Id == orderId) 
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }


    }
}
