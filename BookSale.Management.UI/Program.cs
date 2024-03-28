using BookSale.Management.DataAccess;
using BookSale.Management.DataAccess.Configuration;
using BookSale.Management.DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var builderRazor = builder.Services.AddRazorPages();

builderRazor.AddRazorRuntimeCompilation();
// Đăng ký vào Db SQL
builder.Services.ConfigureIdeitity(builder.Configuration);
//
builder.Services.AddDependencyInjection();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.AutoMigration();
//app.AutoMigration().GetAwaiter().GetResult();

app.SeedData(builder.Configuration).GetAwaiter().GetResult();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    builderRazor.AddRazorRuntimeCompilation();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "AdminRouting",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
