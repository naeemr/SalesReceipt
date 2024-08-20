using Application.Common;
using Application.Common.Interfaces;
using Application.SalesReceipt;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

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
