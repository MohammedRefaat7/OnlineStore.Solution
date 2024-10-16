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
        public ProductWithTypeAndBrandSpecs() : base()
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
        public ProductWithTypeAndBrandSpecs(int id) : base(p => p.Id == id) 
        {
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
    }
}
