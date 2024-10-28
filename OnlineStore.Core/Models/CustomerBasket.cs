using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Models
{
	public class CustomerBasket
	{
		public int Id { get; set; }
		public List<BasketItem> Items { get; set; }
	}
}
