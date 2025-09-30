// Import required namespaces
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Repositories;

// Create a WebApplication builder with command-line args
var builder = WebApplication.CreateBuilder(args);

// Register services with the dependency injection container
builder.Services.AddControllers(); // Add controller support
builder.Services.AddControllersWithViews(); // Add controller and view support
builder.Services.AddEndpointsApiExplorer(); // Enable endpoint API explorer (for Swagger)
builder.Services.AddSwaggerGen(); // Add Swagger for API documentation

// Configure Entity Framework Core to use SQL Server with connection string from configuration
builder.Services.AddDbContext<ProductsDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

// Register the product repository for dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Configure Kestrel web server to listen on HTTPS port 5001
builder.WebHost.ConfigureKestrel(options => {
	options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps());
});

// Build the application
var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment()) {
	// Enable Swagger UI in development environment
	app.UseSwagger();
	app.UseSwaggerUI(c => {
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
		c.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
	});
}

app.UseRouting(); // Enable routing middleware
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.MapControllers(); // Map controller routes


// Add default controller route
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


// Run the application
app.Run();
