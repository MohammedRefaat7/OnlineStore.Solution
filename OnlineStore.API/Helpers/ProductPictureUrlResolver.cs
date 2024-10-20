﻿using AutoMapper;
using OnlineStore.API.DTOs;
using OnlineStore.Core.Models;

namespace OnlineStore.API.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			}
			return string.Empty ;
		}
	}
}
