using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Restaurant.Data;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;
using static NuGet.Packaging.PackagingConstants;

namespace Restaurant.Controllers
{
	public class AdminController : Controller
	{

		private RestaurantContext _context;
		public AdminController(RestaurantContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return RedirectToAction("Users");
		}

		public IActionResult Users()
		{
			var Users = _context.Users;

			return View(Users);
		}

		public IActionResult RemoveUser(int userId)
		{
			var user = _context.Users.Find(userId);

			if (user == null)
			{
				return NotFound();
			}

			foreach (var order in _context.Orders.Where(o => o.UserId == userId))
			{
				_context.Orders.Remove(order);
			}

			foreach (var reservation in _context.Reservations.Where(r => r.UserId == userId))
			{
				_context.Reservations.Remove(reservation);
			}

			_context.Remove(user);
			_context.SaveChanges();

			return RedirectToAction("Users");
		}

		public IActionResult EditUser(int userId)
		{
			var user = _context.Users.Find(userId);

			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		[HttpPost]
		[Authorize]
		public IActionResult EditUser(UserViewModel user)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var user1 = _context.Users.Find(user.Id);
			user1.FName = user.FName;
			user1.LName = user.LName;
			user1.PhoneNumber = user.PhoneNumber;
			user1.Password = user.Password;
			user1.Address = user.Address;
			user1.IsAdmin = user.IsAdmin;

			_context.Update(user1);
			_context.SaveChanges();
			return RedirectToAction("Users");
		}

		public IActionResult Reserves(DateTime? reserveDate)
		{
			var reserves = _context.Reservations.Include(r => r.User).AsQueryable();
            if (reserveDate.HasValue)
            {
                reserves = reserves.Where(o => o.DateAndTime.Date == reserveDate.Value.Date);
            }

            return View(reserves);
		}

		public IActionResult Orders(DateTime? orderDate)
		{
			var orders = _context.Orders
				.Include(r => r.User)
				.Include(r => r.OrderDetails)
				.AsQueryable();

			if (orderDate.HasValue)
			{
				orders = orders.Where(o => o.CreateDate.Date == orderDate.Value.Date);
			}

			var order = orders.Select(order => new OrdersViewModel
			{
				Id = order.Id,
				UserName = order.User.FName + " " + order.User.LName,
				UserAddress = order.User.Address,
				CreateDate = order.CreateDate,
				TotalPrice = order.OrderDetails.Sum(od => od.Price * od.Quantity),
				IsFinal = order.IsFinal
			}).ToList();		

			return View(order);
		}

		public IActionResult OrderDetails(int orderId)
		{
			var order = _context.Orders
				.Where(o => o.Id == orderId)
				.Include(o => o.OrderDetails)
				.ThenInclude(o => o.Food).FirstOrDefault();

			return View(order);
		}

		[HttpGet]
		public JsonResult GetOrdersByDate(DateTime date)
		{

			var orders = _context.Orders.Where(o => o.CreateDate == date.Date).ToList();
			return Json(orders);
		}
	}
}
