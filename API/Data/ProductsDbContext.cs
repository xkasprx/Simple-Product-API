using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Data
{
	// Database context for Products
	public class ProductsDbContext : DbContext
	{
		// Constructor accepting DbContextOptions
		public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

		// DbSet representing the Products table
		public DbSet<Product> Products { get; set; } = null!;
		
	}
}
