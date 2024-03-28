using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Services;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace BookSale.Management.DataAccess.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureIdeitity(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Name = "BookSaleManagementCookie";
                option.ExpireTimeSpan = TimeSpan.FromHours(8);
                option.LoginPath = "/admin/authentication/login";
                option.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(option =>
            {
                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                option.Lockout.MaxFailedAccessAttempts = 3;
            });

            
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationUser>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
