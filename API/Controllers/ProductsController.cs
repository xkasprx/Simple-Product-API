using Microsoft.AspNetCore.Mvc;
using ProductApi.Repositories;

namespace ProductApi.Controllers
{
	// API controller for managing products and handling HTTP requests
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		// Reference to the product repository
		private readonly IProductRepository _repo;

		// Constructor injecting the product repository
		public ProductsController(IProductRepository repo)
		{
			_repo = repo;
		}

		// GET /api/products
		// Retrieves all products
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var products = await _repo.GetAllAsync();
				return Ok(products);
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// GET /api/products/paged?page=1&pageSize=10
		// Retrieves a paged list of products based on query parameters
		[HttpGet("paged")]
		public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			if (page <= 0 || pageSize <= 0)
			{
				return BadRequest("Page and pageSize must be greater than zero.");
			}

			try
			{
				var products = await _repo.GetPagedAsync(page, pageSize);
				return Ok(products);
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// GET /api/products/sp
		// Retrieves products with price under $10 using a stored procedure
		[HttpGet("sp")]
		public async Task<IActionResult> GetProductsUnder10()
		{
			try
			{
				var products = await _repo.GetProductsUnder10Async();
				return Ok(products);
			}
			catch (Exception ex)
			{
				// Log exception here (use ILogger if available)
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
