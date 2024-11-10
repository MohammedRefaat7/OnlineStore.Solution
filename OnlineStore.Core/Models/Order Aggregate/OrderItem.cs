using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Models.Order_Aggregate
{
	public class OrderItem : BaseEntity
	{
        public ProductItemOrdered Product { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
