using OnlineStore.Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications.OrderSpecs
{
	public class OrderSpecifications : BaseSpecification<Order>
	{
        public OrderSpecifications(string BuyerEmail) : base(O => O.BuyerEmail == BuyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.DateTimeOffset);
        }
    }
}

