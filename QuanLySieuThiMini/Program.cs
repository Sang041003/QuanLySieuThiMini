
using Microsoft.EntityFrameworkCore;
using QuanLySieuThiMini.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDBContext>(x =>x.UseSqlServer(builder.Configuration.GetConnectionString("dbconnect")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "create",
    pattern: "{controller=Product}/{action=Create}/{id?}",
    defaults: new { controller = "Product", action = "Create" });

app.Run();
