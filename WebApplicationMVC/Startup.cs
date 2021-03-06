
using Autofac;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationWeb;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Aplication.AplicationServices.Interface;
using Aplication.AplicationServices.Services;

namespace WebApplicationMVC
{

    public class Startup
    {
        [System.Obsolete]
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthenticationCore();

            services.AddCors(options => options.AddPolicy("AllowWebApp",
                          builder => builder.AllowAnyMethod()
                         .AllowAnyMethod()
                         .AllowAnyOrigin()));

            //Obtener la configuracion de appsettings.json
            services.AddSingleton(Configuration.GetSection("Config").Get<Config>());

            services.AddHealthChecks();

            services.AddMemoryCache();

            AddSwagger(services);

            services.AddHealthChecks();

            var appSettingsSection = Configuration.GetSection("Config").Get<Config>();
            var key = Encoding.ASCII.GetBytes(appSettingsSection.appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(30),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false
                };
            });

            services.AddScoped<IUserInfoService, UserInfoService>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {              
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Prueba WEB api",
                    Version = "v1",
                    Description = "Desarrollor:Armando Rodrigues"
                    
                });
            });
        }
       
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Autofact
            builder.RegisterModule<ServiceModules>();
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowWebApp");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/working");


        }

    }
}
