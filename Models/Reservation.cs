using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
	public class Reservation
	{
        public int Id { get; set; }
		[Display(Name = "زمان و تاریخ")]
		public DateTime DateAndTime { get; set; }
		[Display(Name = "تعداد میهمان")]
		public int NumberOfGuest { get; set; }
		[Display(Name = " توضیحات")]
		public string? Description { get; set; }
        public bool IsCanceled { get; set; } = false;


        //Navigation

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

		[Display(Name = " شماره صندلی")]
		public int TableId { get; set; }
        [ForeignKey("TableId")]
        public Table? Table { get; set; }

    }
}
