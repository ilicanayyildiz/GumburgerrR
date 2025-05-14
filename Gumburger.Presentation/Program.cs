
using Gumburger.Application.Services.Abstract;
using Gumburger.Application.Services.AdressServices;
using Gumburger.Application.Services.AppUserServices;
using Gumburger.Application.Services.Concrete;
using Gumburger.Application.Services.ExtraServices;
using Gumburger.Application.Services.MenuServices;
using Gumburger.Application.Services.OrderServices;
using Gumburger.Domain.Entities.Concrete;
using Gumburger.Domain.Enums;
using Gumburger.Domain.Repositories;
using Gumburger.Infrastructure.Context;
using Gumburger.Infrastructure.Repositories;
using Gumburger.Infrastructure.SeedData;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Gumburger.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession();


            builder.Services.AddControllersWithViews();




            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
            builder.Services.AddTransient<IBaseRepository<Address>, AddressRepository>()
                            .AddTransient<IBaseRepository<AppUser>, AppUserRepository>()
                            .AddTransient<IBaseRepository<Extra>, ExtraRepository>()
                            .AddTransient<IBaseRepository<Menu>, MenuRepository>()
                            .AddTransient<IBaseRepository<Order>, OrderRepository>()
                            .AddTransient<IBaseRepository<OrdersMenus>, OrdersMenusRepository>()
                            .AddTransient<IBaseRepository<OrdersExtras>, OrdersExtrasRepository>()
                            .AddTransient<IBaseService<Address>, AddressManager>()
                            .AddTransient<IBaseService<AppUser>, AppUserManager>()
                            .AddTransient<IBaseService<Extra>, ExtraManager>()
                            .AddTransient<IBaseService<Menu>, MenuManager>()
                            .AddTransient<IBaseService<Order>, OrderManager>()
                            .AddTransient<IBaseService<OrdersMenus>, OrdersMenusManager>()
                            .AddTransient<IBaseService<OrdersExtras>, OrdersExtrasManager>()
                            .AddTransient<IAppUserService, AppUserService>()
                            .AddTransient<IMenuService, MenuService>()
                            .AddTransient<IOrderService, OrderService>()
                            .AddTransient<IAdressService, AdressService>()
                            .AddTransient<IExtraService, ExtraService>();


            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".Gumburger.auth";
                    options.ExpireTimeSpan = TimeSpan.FromDays(5);
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Home/AccessDenied";
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

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            SeedDataGenerator.Seed(app, 100, 2, 5, 3, 3);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                var roles = new[] { "Admin" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string email = "admin@admin.com";
                string password = "Admin123+";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    AppUser admin = new AppUser()
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Status = Status.Active,
                        UserName = "Admin",
                        Email = email
                    };

                    await userManager.CreateAsync(admin, password);
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                app.Run();
            }
        }
    }
}



