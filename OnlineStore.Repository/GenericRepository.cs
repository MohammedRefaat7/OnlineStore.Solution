using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;
using OnlineStore.Core.Specifications;
using OnlineStore.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly OnlineStoreDbContext _dbContext;

		public GenericRepository(OnlineStoreDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if(typeof(T) == typeof(Product))
			//{
			//	return (IEnumerable<T>) await _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
			//}
			//else
		    return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> Specs)
		{
			return await ApplySpecification(Specs).ToListAsync();
		}

		public async Task<T> GetByIdAsync(ISpecification<T> Specs)
		{
			return await ApplySpecification(Specs).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> Specs)
		{
			return SpecificationEvalutor<T>.BuildQuery(_dbContext.Set<T>(), Specs);
		}
	}
}
