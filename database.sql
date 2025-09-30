/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

/* Create the database and table */
CREATE DATABASE IF NOT EXISTS "ProductDb";
USE "ProductDb";

/* Create Products table */
CREATE TABLE IF NOT EXISTS "Products" (
	"Id" INT,
	"Name" NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS,
	"Price" DECIMAL(10,2),
	PRIMARY KEY ("Id")
);

/* Insert sample data */
/*!40000 ALTER TABLE "Products" DISABLE KEYS */;
INSERT INTO "Products" ("Id", "Name", "Price") VALUES
	(1, 'Widget', 9.99),
	(2, 'Gadget', 12.49),
	(3, 'Thingamajig', 7.25),
	(4, 'Adapter', 15.5),
	(5, 'Connector', 4.99),
	(6, 'Controller', 22),
	(7, 'Converter', 18.75),
	(8, 'Cradle', 8.5),
	(9, 'Dispenser', 11.25),
	(10, 'Enclosure', 29.99),
	(11, 'Filter', 6),
	(12, 'Holder', 9.5),
	(13, 'Jig', 3.75),
	(14, 'Mount', 14.99),
	(15, 'Panel', 35),
	(16, 'Reducer', 10.5),
	(17, 'Sensor', 19.99),
	(18, 'Stand', 24.5),
	(19, 'Switch', 5.25),
	(20, 'Toolbox', 45);
/*!40000 ALTER TABLE "Products" ENABLE KEYS */;

/* Create stored procedure to get products under $10 */
DELIMITER //
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
END;//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
