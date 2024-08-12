using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}

	public async Task<IEnumerable<Product>> GetProducts(IEnumerable<int> productIds)
	{
		return await _context.Products.Include(p => p.ProductCategory)
			.Where(p => productIds.Contains(p.Id)).ToListAsync();
	}
}
