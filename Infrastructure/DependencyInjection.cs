using Application.Common;
using Application.Common.Interfaces;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));

		services.AddSingleton<JsonHelper>();

		return services;
	}

	public static IServiceCollection AddPersistence(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		options.UseInMemoryDatabase(databaseName: "SalesDB"));

		services.AddScoped<IProductRepository, ProductRepository>();

		return services;
	}
}