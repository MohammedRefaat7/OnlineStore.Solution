using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications
{
	public class ProductWithFilterationSpecForCountAsync : BaseSpecification<Product>
	{
        public ProductWithFilterationSpecForCountAsync( ProductSpecParams Params)
            :base(p =>
					 (
					   (!Params.brandid.HasValue || p.ProductBrandId == Params.brandid)
					   &&
					   (!Params.typeid.HasValue || p.ProductTypeId == Params.typeid)
					 )
				)
		{
            
        }
    }
}
