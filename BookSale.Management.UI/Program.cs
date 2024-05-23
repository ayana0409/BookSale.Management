using BookSale.Management.DataAccess;
using BookSale.Management.DataAccess.Configuration;
using BookSale.Management.UI.Ultility;

var builder = WebApplication.CreateBuilder(args);

var builderRazor = builder.Services.AddRazorPages();

builderRazor.AddRazorRuntimeCompilation();
// Đăng ký vào Db SQL
builder.Services.ConfigureIdeitity(builder.Configuration);
//
builder.Services.AddDependencyInjection();

builder.Services.AddAutoMapper();

builder.Services.AddControllersWithViews(options =>
{
    options.Conventions.Add(new SiteAreaConvention());
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
});

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

app.UseSession();

app.MapControllerRoute(
    name: "AdminRouting",
    pattern: "{area:exists}/{controller=Authentication}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
