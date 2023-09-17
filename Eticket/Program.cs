using Eticket.Data.Cart;
using Eticket.Data.CinemaService;
using Eticket.Data.MovieService;
using Eticket.Data.OrderServices;
using Eticket.Data.ProducerService;
using Eticket.Models;
using ETicket.data;
using ETicket.data.IActorInterfaces;
using ETicket.data.IActorInterfaces.ActorService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IActor, ActorService>();
builder.Services.AddScoped<IProducer, ProducerService>();
builder.Services.AddScoped<ICinema,CinemaService>();
builder.Services.AddScoped<IMovie, MovieServices>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddSession();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EticketDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    //options.LoginPath="Account/Login";
    options.AccessDeniedPath = "Home/Error";
  
});


builder.Services.AddDbContext<EticketDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");
AppDbInitializer.seed(app);


app.Run();
