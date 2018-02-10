using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smart.Services;
using Data.Context;
using Core.Domain.Identity;
using Core.Interfaces;
using Services.Interfaces;
using Data.Repository;
using Services.Entity;
using Smart.Configuration;
using Smart.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System;
using AutoMapper;
using Smart.Mappers;
using Microsoft.Extensions.Options;
using Smart.Extensions.Financial;
using Newtonsoft.Json;
using Smart.Extensions.Invoices;

namespace Smart
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string enUSCulture = "en-US";
        private const string ptBRCulture = "pt-BR";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration["ConnectionStrings:SmartConnection"].ToString();

         
            services.AddDbContext<ContextOnlyGClasse>(options => options.UseSqlServer(connection));


            services.AddDbContext<SmartContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("Smart")));
            //services.AddDbContext<SmartContext>(options => options.UseInMemoryDatabase("local"));

            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");


            services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {
                    config.User.RequireUniqueEmail = true;
                }
                ).AddEntityFrameworkStores<SmartContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "111005312889133";//Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = "bac679d3539f5a822e407e3ae371d2a8";//Configuration["Authentication:Facebook:AppSecret"];
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "830389168682-a93bammk7qanhh8o36nsttg6qb6hhhpi.apps.googleusercontent.com";//Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = "RE4WPzMJClftuhT-TkMDezBp";//Configuration["Authentication:Google:ClientSecret"];
            }).AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "QcrD2f4Mwxoe33XM1xcD2DI3p";//Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = "QxpA54AfH9hZSlt8rWcRPvRWiJqGo7sMbJUWEPHdxw3CumZccb";//Configuration["Authentication:Twitter:ConsumerSecret"];
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo(enUSCulture),
                    new CultureInfo(ptBRCulture)
                };
                options.DefaultRequestCulture = new RequestCulture(ptBRCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{
                //    // My custom request culture logic
                //    return new ProviderCultureResult("en");
                //}));
            });

            services
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services
                .AddDistributedMemoryCache()
                .AddSession();


            //bind services and class

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IServices<>), typeof(Services<>));
            services.AddScoped<IUser, User>();




            services.AddTransient<FinancialExtension>();
            services.AddTransient<InvoiceExtension>();

            // Bind the settings instance as a singleton and expose it as an options type (IOptions<AppSettings>)
            // Note: This ensures that injecting both IOptions<T> and T is made possible and will resolve
            services.Configure<AppSettings>(Configuration);
            services.Configure<AuthMessageSenderOptions>(Configuration);
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            
            app.UseSession();



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
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
