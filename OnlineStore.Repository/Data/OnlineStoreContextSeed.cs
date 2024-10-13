using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Data
{
	public static class OnlineStoreContextSeed
	{
		public static async Task SeedAsync(OnlineStoreDbContext dbContext)
		{
			if (!dbContext.ProductBrands.Any())
			{
				var BrandsData = File.ReadAllText("../OnlineStore.Repository/Data/DataSeed/brands.json");

				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (Brands?.Count > 0)
				{
					foreach (var Brand in Brands)
					{
						await dbContext.Set<ProductBrand>().AddAsync(Brand);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.ProductTypes.Any())
			{
				var TypesData = File.ReadAllText("../OnlineStore.Repository/Data/DataSeed/types.json");

				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

				if (Types?.Count > 0)
				{
					foreach(var Type in Types)
					{
						await dbContext.Set<ProductType>().AddAsync(Type);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.Products.Any())
			{
				var ProductsData = File.ReadAllText("../OnlineStore.Repository/Data/DataSeed/products.json");

				var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

				if (Products?.Count > 0)
				{
					foreach(var P in Products)
					{
						await dbContext.Set<Product>().AddAsync(P);
					}
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
