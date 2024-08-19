using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Service;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddApplication();

		services.AddInfrastructure();

		services.AddPersistence();

		services.AddControllers();

		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Services",
				Version = "v1"
			});
		});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
				"Services v1"));

			CreateInMemoryDB(app);

			var logger = NLog.LogManager.GetLogger("TestLogger");
			logger.Info("NLog configuration loaded.");
		}

		//app.UseMiddleware<ExceptionHandlingMiddleware>();

		//app.UseHttpsRedirection();
		app.UseRouting();

		// global cors policy
		app.UseCors(x => x
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowAnyOrigin());

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}

	private void LogAppConfiguration(IWebHostEnvironment env, IAppLogger _loggrer)
	{
		_loggrer.CreateLogger<Startup>();

		var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json");

		var keys = builder.Build().AsEnumerable().ToList();

		_loggrer.LogInfo("Following are the system configuration: {0}", keys);
	}

	private void CreateInMemoryDB(IApplicationBuilder appBuilder)
	{
		using (var scope = appBuilder.ApplicationServices.CreateScope())
		{
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ApplicationDbContext>();

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			context.CreateData();
		}
	}
}
