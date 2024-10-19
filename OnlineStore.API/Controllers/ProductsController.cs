using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.DTOs;
using OnlineStore.API.Errors;
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
		[ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
		{
			var spec = new ProductWithTypeAndBrandSpecs();
			var Products = await _ProductRepo.GetAllAsync(spec);
			if(Products is null) { return NotFound(new ApiErrorResponse(404)); }
			//Mapping
			var MappedProducts = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products);
			//OkObjectResult result = new OkObjectResult(Products);
			//return result;
			
			return Ok(MappedProducts);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO) , StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse) , 404)] //StatusCodes.Status404NotFound (StatusCodeEnum)
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var specs = new ProductWithTypeAndBrandSpecs(id);
			var Product = await _ProductRepo.GetByIdAsync(specs);
			if (Product is null) return NotFound(new ApiErrorResponse(404));

			//Mapping
			var MappedProduct = _Mapper.Map<Product, ProductToReturnDTO>(Product);

			

			return Ok(MappedProduct);
		}

		#region Check API Exception Response (ServerError)

		//[HttpGet("GetServerError")]
		//public async Task<ActionResult<Product>> GetserverError()
		//{
		//	var specs = new ProductWithTypeAndBrandSpecs(100);
		//	var Product = await _ProductRepo.GetByIdAsync(specs);


		//	//Mapping
		//	var MappedProduct = _Mapper.Map<Product, ProductToReturnDTO>(Product);

		//	var er = MappedProduct.ToString();

		//	return Ok(er);
		//}

		#endregion
	}
}
