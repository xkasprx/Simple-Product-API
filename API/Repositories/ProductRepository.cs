using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Repositories {
	// Repository class for managing Product entities
	public class ProductRepository : IProductRepository
	{
		// Reference to the database context
		private readonly ProductsDbContext _context;

		// Constructor injecting the database context
		public ProductRepository(ProductsDbContext context)
		{
			_context = context;
		}

		// Get all products from the database
		// Returns an IEnumerable of Product, using AsNoTracking for read-only queries
		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			IEnumerable<Product> products;

			try
			{
				products = await _context.Products
					.AsNoTracking()
					.ToListAsync();
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				throw new Exception("Error retrieving all products", ex);
			}

			return products ?? Enumerable.Empty<Product>();
		}

		// Get a paged list of products
		// page: current page number (1-based)
		// pageSize: number of products per page
		// Uses Skip and Take for pagination
		// Ordered by Id to ensure consistent results
		public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
		{
			IEnumerable<Product> pagedProducts;

			if (page <= 0 || pageSize <= 0)
			{
				throw new ArgumentOutOfRangeException("Page and pageSize must be greater than zero.");
			}

			try
			{
				pagedProducts = await _context.Products
					.AsNoTracking()
					.OrderBy(p => p.Id)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				throw new Exception("Error retrieving paged products", ex);
			}

			return pagedProducts ?? Enumerable.Empty<Product>();
		}
		
		// Get products with price under $10 using stored procedure
		public async Task<IEnumerable<Product>> GetProductsUnder10Async()
		{
			IEnumerable<Product> products;

			// Using FromSqlRaw to call the stored procedure
			// Ensure the stored procedure 'usp_GetProductsUnder10' exists in the database
			try
			{
				products = await _context.Products
					.FromSqlRaw("EXEC usp_GetProductsUnder10")
					.AsNoTracking()
					.ToListAsync();
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				throw new Exception("Error retrieving products via stored procedure", ex);
			}

			return products ?? Enumerable.Empty<Product>();
		}
	}
}