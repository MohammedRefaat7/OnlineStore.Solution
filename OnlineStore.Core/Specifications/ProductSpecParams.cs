using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specifications
{
	public class ProductSpecParams
	{
		// string? sort, int? brandid, int? typeid

		public string? sort { get; set; }
        public int? brandid { get; set; }
        public int? typeid { get; set; }

		private int pageSize = 5;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > 10 ? 10 : value < 1 ? 5 : value; }
		}
		private int pageIndex = 1;

		public int PageIndex
		{
			get { return pageIndex; }
			set { pageIndex = value < 1 ? 1 : value; }
		}


	}
}
