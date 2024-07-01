using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsModule.Data;
using NewsModule.Models;
using NewsModule.Services.Jwt;
using System.Configuration;

namespace NewsModule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<NewsModuleContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<NewsModuleContext>();


            //jwt
            JwtOptions jwtOptions = new JwtOptions();

           
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        //ValidIssuer = jwtOptions.issuer,
                        ValidateAudience = false,
                        //ValidAudience = jwtOptions.audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["mycookies"];

                            return Task.CompletedTask;
                        }
                    };
                    
                });

            

            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("OnlyAdmin",policyBuilder=>policyBuilder.RequireClaim("Role","Admin"));
            });
            


            var app = builder.Build();


            app.UseCookiePolicy(new CookiePolicyOptions 
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            /*builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = "/Registration/Login";
                opts.AccessDeniedPath = "/AccessDenied"; //путь к странице с информацией о запрете доступа
            });*/



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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
