using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
	public class LoginViewModel
	{
		[MaxLength(20)]
		[Display(Name = "شماره تلفن")]
		[Required(ErrorMessage = "لطفا شماره تلفن خود را وارد کنید")] 
		public string? PhoneNumber { get; set; }
		[MaxLength(50)]
		[DataType(DataType.Password)]
		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
		public string? Password { get; set; }

		[Display(Name = "مرا به خاطر بسپار")]
		public bool RememberMe { get; set; }
	}
}
