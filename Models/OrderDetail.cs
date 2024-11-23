using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }


        //Navigation Property
        [ForeignKey("FoodId")]
        public Food? Food { get; set; }
        [Required]
        public int FoodId { get; set; }

		[ForeignKey("OrderId")]
		public Order? Order { get; set; }
        public int OrderId { get; set; }
        
    }
}
