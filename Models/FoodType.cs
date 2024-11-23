using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(70)]
        public string? Name { get; set; }

        //Navigation Property
        public List<Food>? Foods { get; set; }

    }
}
