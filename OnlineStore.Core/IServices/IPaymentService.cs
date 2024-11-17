using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.IServices
{
	public interface IPaymentService
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);
	}
}
