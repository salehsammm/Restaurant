using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageName { get; set; }
        public string? Description { get; set; }


        //Navigation
        [ForeignKey("TypeId")]
        public FoodType? FoodType { get; set; }
        public int TypeId { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
