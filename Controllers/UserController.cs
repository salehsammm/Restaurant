using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;
using System.Security.Claims;

namespace Restaurant.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Register(RegisterViewModel register)
		{
			if (!ModelState.IsValid)
			{
				return View(register);
			}

			var user = new User()
			{
				FName = register.FName,
				LName = register.LName,
				PhoneNumber = register.PhoneNumber,
				Address = register.Address,
				Password = register.Password,
				IsAdmin = false,
				RegisterDate = DateTime.Now
			};

			_userRepository.AddUser(user);

			return View("SuccessRegister", register);
		}

		public IActionResult VerifyPhoneNumber(string phoneNumber)
		{
			if (_userRepository.IsExistUserByPhone(phoneNumber.ToLower()))
			{
				return Json($"شماره تلفن {phoneNumber} قبلا ثبت نام کرده است");
			}
			return Json(true);
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel login)
		{
			if (!ModelState.IsValid)
			{
				return View(login);
			}
			var user = _userRepository.GetUserForLogin(login.PhoneNumber.ToLower(), login.Password);
			if (user == null)
			{
				ModelState.AddModelError("PhoneNumber", "اطلاعات وارد شده صحیح نیست");
				return View(login);
			}

			

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
				new Claim("IsAdmin", user.IsAdmin.ToString()),
				new Claim(ClaimTypes.Name,user.FName+" "+user.LName)
            };

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var principal = new ClaimsPrincipal(identity);

			var properties = new AuthenticationProperties
			{				
				IsPersistent = login.RememberMe
			};

			HttpContext.SignInAsync(principal, properties);

			return Redirect("/");
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("/User/Login");
		}
	}
}
