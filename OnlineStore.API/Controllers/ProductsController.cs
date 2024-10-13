﻿using Microsoft.AspNetCore.Http;
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
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
		{
			var Products = await _ProductRepo.GetAllAsync();

			//OkObjectResult result = new OkObjectResult(Products);
			//return result;
			return Ok(Products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var Product = await _ProductRepo.GetByIdAsync(id);
			return Ok(Product);
		}
    }
}
