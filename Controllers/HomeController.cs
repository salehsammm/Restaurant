using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Services;
using System.Diagnostics;
using System.Security.Claims;
using ZarinpalSandbox;

namespace Restaurant.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private RestaurantContext _context;
		private readonly DiscountCalculator _discountCalculator;

		public HomeController(ILogger<HomeController> logger, RestaurantContext context, DiscountCalculator discountCalculator)
		{
			_logger = logger;
			_context = context;
			_discountCalculator = discountCalculator;
		}

		public IActionResult Index()
		{
			decimal discount = 0;

            if (User.Identity.IsAuthenticated)
			{
				int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			    discount = _discountCalculator.CalculateDiscount(userId);				
			}

            ViewBag.Discount = discount;
            return View();
		}

		public IActionResult ViewMenu()
		{
			decimal discount = 0;

			if (User.Identity.IsAuthenticated)
			{
				int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
				discount = _discountCalculator.CalculateDiscount(userId);
			}
			ViewBag.Discount = discount;
			var foods = new MenuViewModel()
			{
				Foods = _context.Foods.Include(f => f.FoodType).ToList(),
				Types = _context.FoodTypes.ToList()
			};

			return View(foods);
		}

		[Authorize]
		public IActionResult ReserveTable()
		{
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			var user = _context.Users.Find(userId);
			var reserveVm = new ReserveViewModel()
			{
				UserName = user.FName + " " + user.LName,
				PhoneNumber = user.PhoneNumber,
				Date = DateTime.Now.Date,
				Time = DateTime.Now.TimeOfDay
			};

			return View(reserveVm);
		}

		[HttpPost]
		public IActionResult ReserveTable(ReserveViewModel reserveVm)
		{
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			var user = _context.Users.Find(userId);

			//if (!ModelState.IsValid)
			//{
			//    return View(reserveVm);
			//}

			DateTime dateTime = reserveVm.Date + reserveVm.Time;
			if (dateTime < DateTime.Now)
			{
				ModelState.AddModelError("Date", "لطفا تاریخ امروز یا روز های آینده را انتخاب نمایید");
				return View(reserveVm);
			}

			int tableCap = _context.Tables.Where(t => t.Id == reserveVm.TableNum).Select(t => t.Capacity).FirstOrDefault();
			if (reserveVm.NumberOfGuest > tableCap)
			{
				ModelState.AddModelError("TableNum", "ظرفیت میز انتخابی از تعداد میهمان های شما کمتر است");
				return View(reserveVm);
			}

			DateTime oneHourBefore = dateTime.AddHours(-1);
			if (_context.Reservations.Any(r =>
				r.DateAndTime >= oneHourBefore &&
				r.DateAndTime <= dateTime &&
				r.TableId == reserveVm.TableNum))
			{
				ModelState.AddModelError("TableNum", "در این تاریخ و زمان این میز قبلا رزرو شده است");
				return View(reserveVm);
			}

			var reserve = new Reservation()
			{
				DateAndTime = dateTime,
				Description = reserveVm.Description,
				NumberOfGuest = reserveVm.NumberOfGuest,
				TableId = reserveVm.TableNum,
				UserId = userId,
				User = user
			};

			_context.Add(reserve);
			_context.SaveChanges();

			return View("Success");
		}

		[Authorize]
		public IActionResult UserReservations()
		{
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			var user = _context.Users.Find(userId);

			var reserves = _context.Reservations.Where(r => r.UserId == userId && !r.IsCanceled)
				.Include(r => r.User).ToList();

			return View(reserves);
		}

		[Authorize]
		public IActionResult RemoveReserve(int reserveId)
		{
			var reserve = _context.Reservations.Find(reserveId);
			_context.Remove(reserve);
			_context.SaveChanges();

			return RedirectToAction("UserReservations");
		}

		public IActionResult NoAccess()
		{
			return View();
		}

		[Authorize]
		public IActionResult ShowCart()
		{								
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			var order = _context.Orders.Where(o => o.UserId == userId && !o.IsFinal)
				.Include(o => o.OrderDetails)
				.ThenInclude(c => c.Food).FirstOrDefault();

			
			decimal discount = _discountCalculator.CalculateDiscount(userId);
			ViewBag.Discount = discount;

			if (order != null) {
				ViewBag.FinalPrice1 = order.OrderDetails.Sum(s => s.Price * s.Quantity);
				ViewBag.FinalPrice2 = ViewBag.FinalPrice1 - (discount/1000);
			}


			return View(order);
		}

		[Authorize]
		public IActionResult AddToCart(int foodId)
		{
			var food = _context.Foods.SingleOrDefault(p => p.Id == foodId);

			if (food != null)
			{
				int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
				var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinal);

				if (order != null)
				{
					var orderDetai = _context.OrderDetails.FirstOrDefault(d => d.Order.Id == order.Id && d.FoodId == food.Id);
					if (orderDetai != null)
					{
						orderDetai.Quantity += 1;
					}
					else
					{
						_context.OrderDetails.Add(new OrderDetail()
						{
							OrderId = order.Id,
							FoodId = food.Id,
							Price = food.Price,
							Quantity = 1
						});
					}
				}
				else
				{
					order = new Order()
					{
						IsFinal = false,
						CreateDate = DateTime.Now,
						UserId = userId,
					};
					_context.Orders.Add(order);
					_context.SaveChanges();
					_context.OrderDetails.Add(new OrderDetail()
					{
						OrderId = order.Id,
						FoodId = food.Id,
						Price = food.Price,
						Quantity = 1
					});
				}

				_context.SaveChanges();
			}
			return RedirectToAction("ShowCart");
		}

		[Authorize]
		public IActionResult Payment()
		{
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var order = _context.Orders
				.Include(o => o.OrderDetails)
				.FirstOrDefault(o => o.UserId == userId && !o.IsFinal);
			if (order == null)
				return NotFound();

			var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
			var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.Id}",
				"http://localhost:44395/Home/OnlinePayment/");
			if (res.Result.Status == 100)
			{
				return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
			}
			else
			{
				return BadRequest();
			}
		}

		public IActionResult OnlinePayment(int id)
		{
			if (HttpContext.Request.Query["Status"] != "" &&
				HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
				HttpContext.Request.Query["Authority"] != "")
			{
				string authority = HttpContext.Request.Query["Authority"].ToString();
				var order = _context.Orders.Include(o => o.OrderDetails)
					.FirstOrDefault(o => o.Id == id);
				var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
				var res = payment.Verification(authority).Result;
	
				if (res.Status == 100)
				{
					order.IsFinal = true;
					_context.Orders.Update(order);
					_context.SaveChanges(); 
					ViewBag.code = res.RefId;
					return View();
				}
			}
			return NotFound();
		}

		[Authorize]
		public IActionResult Payment1(int orderId)
		{
            var order = _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return NotFound();
            order.IsFinal = true;
            _context.Orders.Update(order);
            _context.SaveChanges();

			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			decimal discount = _discountCalculator.CalculateDiscount(userId);
			ViewBag.Discount = discount;

			return View();
		}

		[Authorize]
		public IActionResult RemoveCart(int detailId)
		{
			var orderDetail = _context.OrderDetails.Find(detailId);
			_context.Remove(orderDetail);
			_context.SaveChanges();

			return RedirectToAction("ShowCart");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
