using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDescending { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }

		// GetAll  
		public BaseSpecification()
        {
            //Includes = new List<Expression<Func<T, object>>> ();
        }

		// GetById 
        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
			// Includes = new List<Expression<Func<T, object>>>();
		}

		public void AddOrderBy(Expression<Func<T, object>> OrderbyExpression)
		{
			OrderBy = OrderbyExpression;
		}

		public void AddOrderByDesc(Expression<Func<T, object>> OrderbyDescExpression)
		{
			OrderByDescending = OrderbyDescExpression;
		}

		public void ApplyPagination(int skip , int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}
	}
}
