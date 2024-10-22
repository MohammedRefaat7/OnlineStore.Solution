using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications
{
	public interface ISpecification<T>  where T : BaseEntity
	{
        // Signature of Property for each Component of QUERY ...

        // Where Condition (Filteration) --> (Criteria) ...
        public Expression<Func<T,bool>> Criteria { get; set; }

        //Signature For OrderBy
        public Expression<Func<T,object>> OrderBy { get; set; }

		//Signature For OrderByDesc
		public Expression<Func<T, object>> OrderByDescending { get; set; }

		// List of Expression --> (Includes) ...
		public List<Expression<Func<T , object>>> Includes { get; set; }

        //SKIP(int)
        public int Skip { get; set; }

        // TAKE(int)
        public int Take { get; set; }

        //IsPaginationEnabled
        public bool IsPaginationEnabled { get; set; }

    }
}
