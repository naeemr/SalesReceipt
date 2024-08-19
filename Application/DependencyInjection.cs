using Application.Common;
using Application.Common.Interfaces;
using Application.Sales;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<ISalesReceiptUseCase, SalesReceiptUseCase>();
		services.AddScoped<ReceiptService>();
		services.AddSingleton<IJsonHelper, JsonHelper>();

		return services;
	}
}
