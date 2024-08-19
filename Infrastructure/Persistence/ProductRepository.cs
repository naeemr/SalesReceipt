using Application.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	private readonly IAppLogger _appLogger;

	public ProductRepository(ApplicationDbContext dbContext,
		IAppLogger appLogger) : base(dbContext)
	{
		_appLogger = appLogger;
	}

	public async Task<IEnumerable<Product>> GetProducts(IEnumerable<int> productIds)
	{
		try
		{
			var products = await _context.Products.Include(p => p.ProductCategory)
				.Where(p => productIds.Contains(p.Id)).AsNoTracking().ToListAsync();

			return products;
		}
		catch (Exception ex)
		{
			_appLogger.LogError(ex, string.Format("GetProducts: ProductIds are {0}",
				string.Join(',', productIds.Select(s => s))));

			throw;
		}
	}
}
