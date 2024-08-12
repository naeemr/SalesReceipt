using Application.Common.Interfaces;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IProductRepository : IBaseRepository<Product>
	{
		public Task<IEnumerable<Product>> GetProducts(IEnumerable<int> productIds);
	}
}
