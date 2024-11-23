﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant.Data;

#nullable disable

namespace Restaurant.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    partial class RestaurantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Foods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = " با جو درست میشود",
                            ImageName = "soupJo",
                            Name = "سوپ جو",
                            Price = 120m,
                            Quantity = 20,
                            TypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "در آن کشک و بادمجان وجود دارد",
                            ImageName = "KashkBademjan",
                            Name = "کشک بادمجان",
                            Price = 220m,
                            Quantity = 20,
                            TypeId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "در آن میرزا و قاسم وجود دارد",
                            ImageName = "MirzaGhasemi",
                            Name = "میرزا قاسمی",
                            Price = 180m,
                            Quantity = 20,
                            TypeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Description = "سالادی برای تمام فصول",
                            ImageName = "SeansonSalad",
                            Name = "سالاد فصل",
                            Price = 80m,
                            Quantity = 20,
                            TypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            Description = "دو سیخ کباب",
                            ImageName = "KebabKoobide",
                            Name = " کباب کوبیده",
                            Price = 360m,
                            Quantity = 20,
                            TypeId = 2
                        },
                        new
                        {
                            Id = 8,
                            Description = "جوجه به همراه دورچین",
                            ImageName = "JoojeNoBone",
                            Name = "جوجه کباب بدون استخوان",
                            Price = 320m,
                            Quantity = 20,
                            TypeId = 2
                        },
                        new
                        {
                            Id = 6,
                            Description = "کیکی که در آن شکلات وجود دارد",
                            ImageName = "ChoclateCake",
                            Name = "کیک شکلاتی",
                            Price = 110m,
                            Quantity = 20,
                            TypeId = 3
                        },
                        new
                        {
                            Id = 7,
                            Description = "دوغی که صنعتی نیست",
                            ImageName = "DooghMahali",
                            Name = "دوغ محلی",
                            Price = 39m,
                            Quantity = 20,
                            TypeId = 4
                        });
                });

            modelBuilder.Entity("Restaurant.Models.FoodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.ToTable("FoodTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "پیش غذا"
                        },
                        new
                        {
                            Id = 2,
                            Name = "غذای اصلی"
                        },
                        new
                        {
                            Id = 3,
                            Name = "دسر"
                        },
                        new
                        {
                            Id = 4,
                            Name = "نوشیدنی"
                        });
                });

            modelBuilder.Entity("Restaurant.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinal")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Restaurant.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Restaurant.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfGuest")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Restaurant.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Restaurant.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegisterDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Restaurant.Models.Food", b =>
                {
                    b.HasOne("Restaurant.Models.FoodType", "FoodType")
                        .WithMany("Foods")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodType");
                });

            modelBuilder.Entity("Restaurant.Models.Order", b =>
                {
                    b.HasOne("Restaurant.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Restaurant.Models.OrderDetail", b =>
                {
                    b.HasOne("Restaurant.Models.Food", "food")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("food");
                });

            modelBuilder.Entity("Restaurant.Models.Reservation", b =>
                {
                    b.HasOne("Restaurant.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Restaurant.Models.Food", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Restaurant.Models.FoodType", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("Restaurant.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Restaurant.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
