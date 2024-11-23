using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="نام")]
        public string FName { get; set; }
        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LName { get; set; }
        [Required]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "رمز ورود")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "ادمین")]
        public bool IsAdmin { get; set; }


        //Navigation Property
        //[ForeignKey("OrderId")]
        public List<Order> Orders { get; set; }
        //public int OrderId { get; set; }
    }
}

