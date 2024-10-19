using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models;
using OnlineStore.Repository.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Data
{
	public class OnlineStoreDbContext : DbContext
	{
        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new ProductConfig());
			//modelBuilder.ApplyConfiguration(new ProductBrandConfig());
			//modelBuilder.ApplyConfiguration(new ProductTypeConfig())

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

    }

}
