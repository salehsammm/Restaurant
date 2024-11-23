namespace Restaurant.Models
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
		public string UserName { get; set; }
		public string UserAddress { get; set; }
		public DateTime CreateDate { get; set; }
		public decimal TotalPrice { get; set; }
		public bool IsFinal { get; set; }
	}
}
