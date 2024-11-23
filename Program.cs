using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DbContext

builder.Services.AddDbContext<RestaurantContext>(options =>
    {options.UseSqlServer("Data Source =(local)\\MSSQLSERVER2;Initial Catalog=Restaurant_Db;TrustServerCertificate=True;Integrated Security=true");});

#endregion

builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<DiscountCalculator, DiscountCalculator>();

#region Autentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(option =>
	{
		option.LoginPath = "/User/Login";
		option.LogoutPath = "/User/Logout";
		option.ExpireTimeSpan = TimeSpan.FromDays(60);
	});

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Admin"))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/User/Login");
        }
        else if (!bool.Parse(context.User.FindFirstValue("IsAdmin")))
        {
            context.Response.Redirect("/Home/NoAccess");
        }
    }
    await next.Invoke();
});

app.MapControllerRoute(
    name: "default",
	defaults: null,
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
