using OnlineStore.Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications.OrderSpecs
{
	public class OrderWithPaymentIntentIdSpec : BaseSpecification<Order>
	{
        public OrderWithPaymentIntentIdSpec(string PaymentIntentId) : base(o => o.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
