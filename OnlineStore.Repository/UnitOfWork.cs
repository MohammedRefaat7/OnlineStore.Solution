using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Core;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;
using OnlineStore.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OnlineStoreDbContext _dbContext;

		private readonly Lazy<Hashtable> _repositories;
		public UnitOfWork(OnlineStoreDbContext dbContext)
        {
			_dbContext = dbContext;
			_repositories = new Lazy<Hashtable>();
		}
        public async Task<int> CompleteAsync()
			=> await _dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync()
			=> _dbContext.DisposeAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var TypeName = typeof(TEntity).Name;

			if (!_repositories.Value.ContainsKey(TypeName))
			{
				var RepositoryObj = new GenericRepository<TEntity>(_dbContext);
				_repositories.Value.Add(TypeName, RepositoryObj);
			}
			return _repositories.Value[TypeName] as IGenericRepository<TEntity> ??
			       throw new InvalidOperationException($"Repository for type {TypeName} not found.");
		}
	}
}
