using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFinal { get; set; }

        //Navigation Property
        public List<OrderDetail>? OrderDetails { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }
        public int UserId { get; set; }
    }
}

