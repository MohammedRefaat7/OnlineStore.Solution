﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models;
using OnlineStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
	public static class SpecificationEvalutor<T> where T : BaseEntity
	{
		public static IQueryable<T> BuildQuery(IQueryable<T> EntryPoint , ISpecification<T> Specs)
		{
			var Query = EntryPoint;    // db.Context.Set<T>

			if(Specs.Criteria is not null)
			{
				Query = Query.Where(Specs.Criteria);
			}

			// _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
			Query = Specs.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

			return Query; 
		}
	}
}
