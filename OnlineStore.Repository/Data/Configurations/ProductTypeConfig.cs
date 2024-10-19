using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Data.Configurations
{
	internal class ProductTypeConfig : IEntityTypeConfiguration<ProductType>
	{
		public void Configure(EntityTypeBuilder<ProductType> builder)
		{
			builder.Property(P => P.Name).IsRequired();
		}
	}
}
