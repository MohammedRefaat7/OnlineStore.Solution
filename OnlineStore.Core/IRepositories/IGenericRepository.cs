﻿using OnlineStore.Core.Models;
using OnlineStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.IRepositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);

		Task<IEnumerable<T>> GetAllAsync(ISpecification<T> Specs);
		Task<T> GetByIdAsync(ISpecification<T> Specs);
	}
}
