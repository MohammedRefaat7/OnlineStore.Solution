using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Errors;
using OnlineStore.API.Helpers;
using OnlineStore.Core;
using OnlineStore.Core.IRepositories;
using OnlineStore.Core.IServices;
using OnlineStore.Repository;
using OnlineStore.Service;

namespace OnlineStore.API.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services) 
		{
			//builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
			//builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
			Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			// builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles() ));
			Services.AddAutoMapper(typeof(MappingProfiles));

			Services.AddScoped<IUnitOfWork, UnitOfWork>();

			Services.AddScoped<IOrderService, OrderService>();

			#region Error Handling
			Services.Configure<ApiBehaviorOptions>(Options =>
			{
				// BehaviorOfModelStateResponseFactory
				Options.InvalidModelStateResponseFactory = (ActionContext) =>
				{
					var Errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToList();

					var ApiValidationError = new ApiValidationErrorResponse()
					{
						Errors = Errors
					};
					return new BadRequestObjectResult(ApiValidationError);
				};
			});
			#endregion

			return Services;
		}
	}
}
