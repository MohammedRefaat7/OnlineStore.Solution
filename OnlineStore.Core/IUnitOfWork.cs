using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		Task<int> CompleteAsync();
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
	}
}
