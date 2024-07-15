using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakip.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connetionsString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<StokTakipContext>(opt => opt.UseSqlServer(connetionsString));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = "/Login/Index";
                    opts.LogoutPath = "/Login/Logout";
                    opts.Cookie.Name = "Auth";
                    opts.SlidingExpiration = false;
                    opts.ExpireTimeSpan = TimeSpan.FromDays(2);
                    opts.AccessDeniedPath = "/Login/AccessDenied";
                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
