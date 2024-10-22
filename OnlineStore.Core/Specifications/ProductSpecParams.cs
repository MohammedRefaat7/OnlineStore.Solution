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
    }
}
