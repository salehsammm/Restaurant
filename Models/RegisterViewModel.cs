using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="لطفا نام خود را وارد کنید")]
        [Display(Name ="نام")]
        public string? FName { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string? LName { get; set; }
        [Required(ErrorMessage = "لطفا شماره تلفن خود را وارد کنید")]
        [Display(Name = "شماره تلفن")]
		[Remote("VerifyPhoneNumber", "User")]
		public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string? Password { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="با کلمه عبور یکسان نیست")]
        [Display(Name = "تکرار کلمه عبور")]
        public string? RePassword { get; set; }
        [Required(ErrorMessage = "لطفا آدرس خود را وارد کنید")]
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
    }
}
