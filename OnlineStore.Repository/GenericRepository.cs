﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.Models;
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
        public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
