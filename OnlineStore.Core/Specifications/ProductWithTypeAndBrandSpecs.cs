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
		//GetAllProducts
        public ProductWithTypeAndBrandSpecs(ProductSpecParams Params ) : 
			base(p =>
			         (
			           (!Params.brandid.HasValue || p.ProductBrandId == Params.brandid) 
			           &&
			           (!Params.typeid.HasValue || p.ProductTypeId == Params.typeid )
			         )
				)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
			
			if (!string.IsNullOrEmpty(Params.sort))
			{
				switch (Params.sort)
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

			// ProductRecords = 100  
			// PageSize = 10 
			// PageIndex = 3
			// skip 20 = ( Pagesize * (PageIndex - 1))  (10 * 2)
			// Take 10 = ( PageSize )                     (10)

			ApplyPagination(Params.PageSize*(Params.PageIndex - 1) , Params.PageSize);
		}

		//GetProductById
        public ProductWithTypeAndBrandSpecs(int id) : base(p => p.Id == id) 
        {
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}


    }
}
