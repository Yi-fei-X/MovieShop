using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Use this method for dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC(Inversion of control) 
            services.AddControllersWithViews();
            services.AddScoped<IMovieService, MovieService>();  // For IMovieService interface, use MovieService class. (If you want to use other implementation, change it here)
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            
            services.AddHttpContextAccessor();  // == AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            // Inject connection string from appsetting.json to MovieShopDbContext
            services.AddDbContext<MovieShopDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));
            // Options of the cookies. We are not creating the cookies here.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>      //Inject some information to the cookies
            {
                options.Cookie.Name = "MovieShopAuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/account/login";   //When the cookie is invalid, go to this page.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();   //middleware, add by default.

            app.UseRouting();
            // The order of middlewares are important. The order matters, http request will go one by one.
            app.UseAuthentication();    //middleware. First check if user is authenticated.
            app.UseAuthorization();     //middleware. Then check if user is authorized.

            app.UseEndpoints(endpoints =>   // Typically the last middleware.
            {
                endpoints.MapControllerRoute(   //routing method
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //you can spcify the default method here
            });
        }
    }
}
