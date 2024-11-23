using Restaurant.Data;
using Restaurant.Models;
using System.Security.Claims;

namespace Restaurant.Services
{
	public class DiscountCalculator
	{
		private RestaurantContext _context;

		public DiscountCalculator(RestaurantContext context)
		{
			_context = context;
		}

		public int CalculateDiscount(int userId)
		{
			var orders = _context.Orders.Where(o => o.UserId == userId).ToList();

			if (orders==null)
			{
				return 0;
			}

			int OrderAmountLastMonth = orders
				.Count(o => o.CreateDate >= DateTime.Today.AddMonths(-1) && o.IsFinal);

			if (OrderAmountLastMonth > 30)
			{
				OrderAmountLastMonth = 30;
			}

			int discount = 1000 * OrderAmountLastMonth;

			return discount;
		}
	}
}
