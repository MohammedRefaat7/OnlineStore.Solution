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
    }
}
