using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications
{
	public class ProductWithTypeAndBrandSpecs : BaseSpecification<Product>
	{
        public ProductWithTypeAndBrandSpecs(string? sort = null) : base()
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "PriceAsc":
					case "Price":
						AddOrderBy(p => p.Price);
						break;
					case "PriceDesc":
						AddOrderByDesc(p => p.Price);
						break;
					case "NameAsc":
					case "Name":
						AddOrderBy(p => p.Name);
						break;
					case "IdDesc":
						AddOrderByDesc(p => p.Id);
						break;
					default:
						AddOrderBy(p => p.Id);
						break;

				}
			}
		}
        public ProductWithTypeAndBrandSpecs(int id) : base(p => p.Id == id) 
        {
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}


    }
}
