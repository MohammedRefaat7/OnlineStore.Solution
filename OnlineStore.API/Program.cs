
using Microsoft.EntityFrameworkCore;
using OnlineStore.Repository.Data;

namespace OnlineStore.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			#region Configure Services Add services to the container.

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			#endregion

			var app = builder.Build();

			#region Update-Database (With Run If we have New Migrations)
			using var Scope = app.Services.CreateScope(); 
			// Group Of Services Lifetime Scopped

			var Services = Scope.ServiceProvider;
			// Services it Self

			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var DbContext = Services.GetRequiredService<OnlineStoreDbContext>();
				// Ask CLR For Creating Object form DbContext Explicitly

				await DbContext.Database.MigrateAsync(); // Apply pending migrations
			}
			catch (Exception ex)
			{
				//var logger = Services.GetRequiredService<ILogger<Program>>();
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An Error Occurred during Migration");
			}
			
			#endregion

			#region Configure the HTTP request pipeline [Middleware]. 

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
