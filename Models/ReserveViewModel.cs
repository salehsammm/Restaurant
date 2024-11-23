using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
	public class ReserveViewModel
	{
		[Display(Name = "نام مشتری")]
		public string? UserName { get; set; }

		[Display(Name = "تلفن همراه")]
		public string? PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "تاریخ")]
		public DateTime Date { get; set; }

		[DataType(DataType.Time)]
        [Display(Name = "زمان")]
        public TimeSpan Time { get; set; }

        [Display(Name = "تعداد میهمان")]
		public int NumberOfGuest { get; set; } = 1;

		[Display(Name = "شماره میز")]
		public int TableNum { get; set; } = 1;

        [Display(Name = " توضیحات")]
		public string? Description { get; set; }

        //public User? User { get; set; }

    }
}
