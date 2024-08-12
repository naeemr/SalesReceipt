using Application.Common;
using Application.Common.Interfaces;
using Application.Sales;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IShoppingCart, ShoppingCart>();
			services.AddScoped<ReceiptService>();
			services.AddScoped<SalesTaxService>();
			services.AddSingleton<IJsonHelper, JsonHelper>();

			return services;
		}
	}
}
