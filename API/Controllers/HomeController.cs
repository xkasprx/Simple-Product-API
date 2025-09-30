using Microsoft.AspNetCore.Mvc;
using ProductApi.Repositories;

namespace ProductApi.Controllers
{
	// HomeController handles requests for the home page
	public class HomeController : Controller
	{
		// Dependency injection for the product repository
		private readonly IProductRepository _repo;

		// Constructor receives the repository implementation
		public HomeController(IProductRepository repo)
		{
			_repo = repo;
		}

		// Handles GET requests to the home page
		public async Task<IActionResult> Index()
		{
			try
			{
				// Retrieve all products asynchronously from the repository
				var products = await _repo.GetAllAsync();
				// Pass the products to the view for rendering
				return View(products);
			}
			catch (Exception ex)
			{
				// Optionally log the exception here
				// Return an error view or status code
				return StatusCode(500, $"An error occurred while retrieving products.\n{ex.Message}");
			}
		}
	}
}
