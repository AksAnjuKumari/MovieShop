using MovieStore_Chili.Models;

using Microsoft.EntityFrameworkCore;
using MovieStore_Chili.Data;
using MovieStore_Chili.Services;

namespace MovieStore_Chili
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            // var connectionString = builder.Configuration.GetConnectionString(
            // "LexiconConnection") ?? throw new InvalidCastException("Lexicon Connection not found");

            var connectionString = builder.Configuration.GetConnectionString(
            "DefaultConnection") ?? throw new InvalidCastException("Default Connection not found");

            builder.Services.AddDbContext<MovieDbContext>(
                options => options.UseSqlServer(connectionString)
                );

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            //builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movie}/{action=Index}");

            app.Run();
        }
    }
}
