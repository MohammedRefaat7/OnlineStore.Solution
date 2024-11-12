using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models;
using OnlineStore.Core.Models.Order_Aggregate;
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

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  //Already Applied (For All Configurations)
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }

}
