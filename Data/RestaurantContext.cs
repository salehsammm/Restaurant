using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }

        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>(
                f => f.Property(f => f.Price).HasColumnType("Money"));

            modelBuilder.Entity<OrderDetail>(
                o => o.Property(o => o.Price).HasColumnType("Money"));

            #region Seed Data

            modelBuilder.Entity<FoodType>().HasData(new FoodType()
            {
                Id = 1,
                Name = "پیش غذا"
            },
            new FoodType()
            {
                Id = 2,
                Name = "غذای اصلی"
            },
            new FoodType()
            {
                Id = 3,
                Name = "دسر"
            },
            new FoodType()
            {
                Id = 4,
                Name = "نوشیدنی"
            });

            modelBuilder.Entity<Food>().HasData(new Food()
            {
                Id = 1,
                Name = "سوپ جو",
                TypeId = 1,
                Description = " با جو درست میشود",
                Price = 120,
                Quantity = 20,
                ImageName = "soupJo"
            },
            new Food()
            {
                Id = 2,
                Name = "کشک بادمجان",
                TypeId = 1,
                Description = "در آن کشک و بادمجان وجود دارد",
                Price = 220,
                Quantity = 20,
                ImageName = "KashkBademjan"
            },
            new Food()
            {
                Id = 3,
                Name = "میرزا قاسمی",
                TypeId = 1,
                Description = "در آن میرزا و قاسم وجود دارد",
                Price = 180,
                Quantity = 20,
                ImageName = "MirzaGhasemi"
            },
            new Food()
            {
                Id = 4,
                Name = "سالاد فصل",
                TypeId = 1,
                Description = "سالادی برای تمام فصول",
                Price = 80,
                Quantity = 20,
                ImageName = "SeansonSalad"
            },
            new Food()
            {
                Id = 5,
                Name = " کباب کوبیده",
                TypeId = 2,
                Description = "دو سیخ کباب",
                Price = 360,
                Quantity = 20,
                ImageName = "KebabKoobide"
            },
			new Food()
			{
				Id = 8,
				Name = "جوجه کباب بدون استخوان",
				TypeId = 2,
				Description = "جوجه به همراه دورچین",
				Price = 320,
				Quantity = 20,
				ImageName = "JoojeNoBone"
			},
			new Food()
            {
                Id = 6,
                Name = "کیک شکلاتی",
                TypeId = 3,
                Description = "کیکی که در آن شکلات وجود دارد",
                Price = 110,
                Quantity = 20,
                ImageName = "ChoclateCake"
            },
            new Food()
            {
                Id = 7,
                Name = "دوغ محلی",
                TypeId = 4,
                Description = "دوغی که صنعتی نیست",
                Price = 39,
                Quantity = 20,
                ImageName = "DooghMahali"
            });

            #endregion
        }

    }
}