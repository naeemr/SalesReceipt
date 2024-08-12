using Application.Common;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Service;
using System;
using System.Net.Http;

namespace IntegrationTest.Infrastructure
{
	public class IntegrationTestFixture : IDisposable
	{
		public readonly ApplicationDbContext DbContext;
		public readonly JsonHelper _jsonHelper;
		private readonly WebApplicationFactory<Startup> _factory;

		public IntegrationTestFixture()
		{
			_factory = new CustomWebApplicationFactory<Startup>();

			DbContext = _factory.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

			_jsonHelper = _factory.Services.GetService(typeof(IJsonHelper)) as JsonHelper;
		}

		public void Dispose()
		{
			DbContext?.Dispose();
			_factory.Dispose();
		}

		public HttpClient HttpClient => _factory.CreateClient();
	}
}