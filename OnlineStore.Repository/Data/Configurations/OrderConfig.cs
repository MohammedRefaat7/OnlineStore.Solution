using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Data.Configurations
{
	public class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(O => O.status)
		        .HasConversion(OStatus => OStatus.ToString(), OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

			builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

			builder.OwnsOne(O => O.ShippingAddress, ShA => ShA.WithOwner());

			builder.HasOne(O => O.DeliveryMethod)
				   .WithMany()
				   .OnDelete(DeleteBehavior.NoAction);
		}
	}
}
