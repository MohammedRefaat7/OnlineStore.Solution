using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;
using OnlineStore.Core.Specifications;

namespace OnlineStore.API.Controllers
{

	public class ProductsController : APIBaseController
	{
		private readonly IGenericRepository<Product> _ProductRepo;
		private readonly IMapper _Mapper;

		public ProductsController(IGenericRepository<Product> ProductRepo , IMapper mapper)
		{
			_ProductRepo = ProductRepo;
			_Mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
		{
			var spec = new ProductWithTypeAndBrandSpecs();
			var Products = await _ProductRepo.GetAllAsync(spec);
			//Mapping
			var MappedProducts = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products);
			//OkObjectResult result = new OkObjectResult(Products);
			//return result;
			return Ok(MappedProducts);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var specs = new ProductWithTypeAndBrandSpecs(id);
			var Product = await _ProductRepo.GetByIdAsync(specs);
			//Mapping
			var MappedProduct = _Mapper.Map<Product, ProductToReturnDTO>(Product);	

			return Ok(MappedProduct);
		}
    }
}
