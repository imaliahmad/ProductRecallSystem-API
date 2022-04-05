using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProductRecallSystem.BLL;
using ProductRecallSystem.BOL;
using ProductRecallSystem.DAL;
using ProductRecallSystem.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRecallSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           services.AddDbContext<EFCodeDbContext>();
            services.AddSwaggerDocument();
            services.AddCors();

            services.AddTransient<IManufacturersDb, ManufacturersDb>();
            services.AddTransient<IManufacturersBs, ManufacturersBs>();

            services.AddIdentity<AppUsers, IdentityRole>()
                .AddEntityFrameworkStores<EFCodeDbContext>()
                .AddDefaultTokenProviders();

            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            services.AddControllersWithViews(x => x.Filters.Add(new AuthorizeFilter(policy)));

            #region JWT Authentication
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Module-Secret-Key"));
            var tokenValidationParameter = new TokenValidationParameters()
            {
                IssuerSigningKey = signInKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(jwt => { jwt.TokenValidationParameters = tokenValidationParameter; });
            #endregion


            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.LoginPath = "/Security/Login";
            //    opt.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = redirectContext =>
            //        {
            //            redirectContext.HttpContext.Response.StatusCode = 401;
            //            return Task.CompletedTask;
            //        },
            //        OnRedirectToAccessDenied = redirectContext =>
            //        {

            //            redirectContext.HttpContext.Response.StatusCode = 401;
            //            return Task.CompletedTask;
            //        },
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseStatusCodePagesWithReExecute("/ErrorLog/{0}");
            //}

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseCors(opt => opt.AllowCredentials()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    .SetIsOriginAllowed(origin => true));


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
