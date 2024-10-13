using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;

namespace OnlineStore.API.Controllers
{
	
	public class ProductsController : APIBaseController
	{
		private readonly IGenericRepository<Product> _ProductRepo;

		public ProductsController(IGenericRepository<Product> ProductRepo)
        {
			_ProductRepo = ProductRepo;
		}


    }
}
