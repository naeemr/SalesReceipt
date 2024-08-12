using Application.Common;
using Infrastructure;
using Infrastructure.Persistence;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest.Infrastructure
{
	[Collection("Sales Tax Integration Tests")]
	public class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
	{
		protected ITestOutputHelper Output { get; }
		protected ApplicationDbContext DbContext { get; }
		protected JsonHelper _jsonHelper { get; }

		private IntegrationTestFixture _fixture;
		protected IntegrationTestBase(IntegrationTestFixture integrationTestFixture, ITestOutputHelper output)
		{
			Output = output;
			DbContext = integrationTestFixture.DbContext;
			_fixture = integrationTestFixture;
			_jsonHelper = integrationTestFixture._jsonHelper;

			if (DbContext != null)
			{
				DbContext.Database.EnsureDeleted();
				DbContext.Database.EnsureCreated();

				DbContext.CreateData();
			}
		}

		public RequestBuilder NewRequest => new RequestBuilder(_fixture.HttpClient);
	}
}