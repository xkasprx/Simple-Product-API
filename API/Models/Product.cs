using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
	// Product entity representing a product in the database
    [Table("Products")]
    public class Product
    {
		// Primary key
        [Key]
        public int Id { get; set; }

		// Name of the product, required with max length of 100
		[Required, MaxLength(100)]
        public string Name { get; set; } = null!;

		// Price of the product, required with decimal type
		[Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
