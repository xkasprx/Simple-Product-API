# Simple Product API

## Overview
A simple ASP.NET Core Web API to fetch product data from a local SQL Server database.  
Includes endpoints to fetch all products, paged results, and data via a stored procedure.

## Setup Instructions
1. Install [.NET 9 SDK](https://dotnet.microsoft.com/download).
2. Ensure a local SQL Server instance is running.
3. Create the database and table:
   ```sql
   CREATE DATABASE ProductDb;
   USE ProductDb;
   CREATE TABLE Products (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name NVARCHAR(100) NOT NULL,
		Price DECIMAL(10,2) NOT NULL
	);
	INSERT INTO Products (Name, Price) VALUES
		('Widget', 9.99),
		('Gadget', 12.49),
		('Thingamajig', 7.25),
		('Adapter', 15.50),
		('Connector', 4.99),
		('Controller', 22.00),
		('Converter', 18.75),
		('Cradle', 8.50),
		('Dispenser', 11.25),
		('Enclosure', 29.99),
		('Filter', 6.00),
		('Holder', 9.50),
		('Jig', 3.75),
		('Mount', 14.99),
		('Panel', 35.00),
		('Reducer', 10.50),
		('Sensor', 19.99),
		('Stand', 24.50),
		('Switch', 5.25),
		('Toolbox', 45.00);
   ```
   
4. Create the stored procedure used by the API:
   ```sql
	CREATE PROCEDURE usp_GetProductsUnder10
	AS
	BEGIN
	    SET NOCOUNT ON;
	    
	    SELECT 
	        Id, 
	        Name, 
	        Price
	    FROM 
	        Products
	    WHERE 
	        Price < 10
	    ORDER BY 
	        Price ASC;
	END;
   ```

_Note: Included is a `database.sql` file that can also be utilized._

5. Update `appsettings.json` with your connection string - don't forget to remove `.template` from the file name.
6. Trust HTTPS dev certificate:

   ```bash
   dotnet dev-certs https --trust
   ```
7. Build & run:

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
8. Open browser: 
   - Swagger UI: [https://localhost:5001/swagger](https://localhost:5001/swagger)
   - MVC Home: [https://localhost:5001/](https://localhost:5001/)

## API Endpoints

| Endpoint                                     | Description                                                             |
| -------------------------------------------- | ----------------------------------------------------------------------- |
| GET `/api/products`                          | Returns all products                                                    |
| GET `/api/products/paged?page=1&pageSize=10` | Returns paged products                                                  |
| GET `/api/products/sp`                       | Returns products fetched via stored procedure `usp_GetProductsUnder10`  |

## Assumptions

* Using local SQL Server on default port (1433).
* Using EF Core for DB access.
* .NET 9 is installed.
* Stored procedure `usp_GetProductsUnder10` exists in the database.

## .NET Version

* Target Framework: .NET 9

## Optional / Bonus Features

* Stored procedure `usp_GetProductsUnder10` exists in DB.
* Pagination endpoint implemented.
* All repository methods use `async/await` for asynchronous calls.
* Error handling included
* Added MVC View

## Project Structure

- **/API**: Contains all API-related code (controllers, models, views, and startup configuration).
- **/Tests**: Contains all unit tests for the API (tests for controllers, repositories, etc.).

## Running Tests

To execute the automated tests, navigate to the Tests folder and run:

```bash
 dotnet test
```

This command builds the test project located in the **/Tests** folder and runs all tests.
