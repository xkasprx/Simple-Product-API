using ProductApi.Models;

namespace ProductApi.Repositories
{
	// Interface defining methods for Product repository
	public interface IProductRepository
	{
		// Get all products
		Task<IEnumerable<Product>> GetAllAsync();

		// Get a paged list of products
		Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize);

		// Get products with price under $10 using stored procedure
		Task<IEnumerable<Product>> GetProductsUnder10Async();
	}
}
