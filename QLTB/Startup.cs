using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLTB.Data;
using QLTB.Data.Models;
using QLTB.Data.Repository;

namespace QLTB
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
            //services.AddSingleton<ILoggerManager, LoggerManager>(); ILoggerManager
            //services.AddControllers();
            //    services.Configure<CookiePolicyOptions>(options =>
            //    {
            //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //        options.CheckConsentNeeded = context => false;
            //        options.MinimumSameSitePolicy = SameSiteMode.None;
            //    });

            services.AddDbContext<QLTBITDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<QLTBITDbContext>();



            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                //    // Lockout settings.
                //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //    options.Lockout.MaxFailedAccessAttempts = 5;
                //    options.Lockout.AllowedForNewUsers = true;

                //    // User settings.
                //    options.User.AllowedUserNameCharacters =
                //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //    options.User.RequireUniqueEmail = false;
            });

            services.AddTransient<ILoaiThietBiRepository, LoaiThietBiRepository>();
            services.AddTransient<IBanGiaoRepository, BanGiaoRepository>();
            services.AddTransient<IChiNhanhRepository, ChiNhanhRepository>();
            services.AddTransient<IChiTietBanGiaoRepository, ChiTietBanGiaoRepository>();
            services.AddTransient<ILoaiPhanMemRepository, LoaiPhanMemRepository>();
            services.AddTransient<INhanVienRepository, NhanVienRepository>();
            services.AddTransient<IPhanMemRepository, PhanMemRepository>();
            services.AddTransient<IThietBiRepository, ThietBiRepository>();
            services.AddTransient<IVanPhongRepository, VanPhongRepository>();
            services.AddTransient<INhapKhoRepository, NhapKhoRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc(
                options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()    // reuired login in all action method
                                 .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "100548929884-109mco1mku83rb2hhn4arf07hnqpuhmc.apps.googleusercontent.com";
                    options.ClientSecret = "FMGzQF_PB1xmXhoo1tpAlXC-";
                    // options.CallbackPath = "";
                })
                .AddFacebook(options =>
                {
                    options.ClientId = "2234668953508404";
                    options.ClientSecret = "a7ad7774ee63251abd8ec4f3cebd1646";
                    // options.CallbackPath = "";
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied"); // Change AccessDenied route
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
                //options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role", "true"));
                //options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role", "true"));

                options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));                
                options.AddPolicy("EditCNRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin") ||
                    context.User.IsInRole("Admin")
                    ));
                
                
                
                options.AddPolicy("CreateRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim => claim.Type == "Create Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));
                options.AddPolicy("CreateCNRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") ||
                    context.User.HasClaim(claim => claim.Type == "Create Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));
                
                
                
                options.AddPolicy("DeleteRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim => claim.Type == "Delete Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));
                options.AddPolicy("DeleteCNRolePolicy", policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") ||
                    context.User.HasClaim(claim => claim.Type == "Delete Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin")
                    ));
                


                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("SuperAdminRolePolicy", policy => policy.RequireRole("Super Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
