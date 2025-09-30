using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repositories;
using System.Linq;
using Xunit;

namespace Tests.Repositories
{
	// Unit tests for ProductRepository
	public class ProductRepositoryTests
	{
		// Creates an in-memory database context and seeds it with sample data
		private ProductsDbContext GetInMemoryDbContext()
		{
			var options = new DbContextOptionsBuilder<ProductsDbContext>()
				.UseInMemoryDatabase(databaseName: "TestProductsDb")
				.Options;

			var context = new ProductsDbContext(options);

			// Seed sample data
			context.Products.AddRange(
				new Product { Id = 1, Name = "Widget", Price = 9.99M },
				new Product { Id = 2, Name = "Gadget", Price = 12.49M }
			);
			context.SaveChanges();
			return context;
		}

		// Tests that GetAllAsync returns all products from the repository
		[Fact]
		public async Task GetAllAsync_ReturnsAllProducts()
		{
			var context = GetInMemoryDbContext();
			var repo = new ProductRepository(context);

			var products = await repo.GetAllAsync();

			Assert.NotNull(products);
			Assert.Equal(2, products.Count()); // using Count() extension method
			Assert.Contains(products, p => p.Name == "Widget");
		}
	}
}
