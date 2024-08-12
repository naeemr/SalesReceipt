using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace Service;

public class Program
{
	public static void Main(string[] args)
	{
		// NLog: setup the logger first to catch all errors
		var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
		try
		{
			CreateHostBuilder(args).Build().Run();
		}
		catch (SqlException ex)
		{
			//NLog: catch setup errors
			if (ex.Number == 2 || ex.Number == 53)
			{
				logger.Fatal(ex, "A SQL network connection error has been occurred, please look into it on priority.");
			}
			throw;
		}
		catch (Exception ex)
		{
			//NLog: catch setup errors
			logger.Fatal(ex, "Stopped program because of exception");
			throw;
		}
		finally
		{
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			NLog.LogManager.Shutdown();
		}
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			}).ConfigureLogging(logBuilder =>
			{
				logBuilder.ClearProviders();
			})
			.UseNLog();
}
