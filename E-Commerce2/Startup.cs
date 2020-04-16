﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Serilog.RequestLoggingMiddleware;
using AutoMapper;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using E_Commerce2.Servcies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce2
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddIdentity<User,IdentityRole>()
           .AddEntityFrameworkStores<DataContext>()
             .AddDefaultUI().AddDefaultTokenProviders(); ;
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRepositoryServcies();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            // app.UseSerilogRequestLogging();
            app.UseCookiePolicy();


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //    name: "areas",
            //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //  );

               
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "areasCa",
                     template: "{area=Admin}/{controller=Catergory}/{action=Categories}/{id?}"
                   );
                routes.MapRoute(
                      name: "areas",
                      template: "{area=Admin}/{controller=Home}/{action=IndexMain}/{id?}"
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
