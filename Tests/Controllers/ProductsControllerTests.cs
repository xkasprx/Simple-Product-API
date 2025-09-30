using Moq;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Controllers
{
	public class ProductsControllerTests
	{
		/// Tests that the Get method returns an OkObjectResult containing a list of products.
		[Fact]
		public async Task Get_ReturnsOkResult_WithListOfProducts()
		{
			// Arrange: Set up a mock repository to return a predefined list of products.
			var mockRepo = new Moq.Mock<IProductRepository>();
			mockRepo.Setup(repo => repo.GetAllAsync())
					.ReturnsAsync(new List<Product> {
						new Product { Id = 1, Name = "Widget", Price = 9.99M },
						new Product { Id = 2, Name = "Gadget", Price = 12.49M }
					});

			var controller = new ProductsController(mockRepo.Object);

			// Act: Call the Get method.
			var result = await controller.GetAll();

			// Assert: Verify the result is OkObjectResult and contains the expected products.
			var okResult = Assert.IsType<OkObjectResult>(result);
			var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
			Assert.Equal(2, System.Linq.Enumerable.Count(products));
		}
	}
}
