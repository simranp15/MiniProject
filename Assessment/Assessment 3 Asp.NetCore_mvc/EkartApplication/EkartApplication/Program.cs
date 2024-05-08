using EkartApplication.Models;
using EkartApplication.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Fetch the connection string from AppSetting.json file
builder.Services.AddDbContext<NorthwindContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


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
