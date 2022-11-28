using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Trading.Data.Common;
using Trading.Data.Database;
using TradingAPI.DependencyRegister;
using TradingAPI.Extensions;
using TradingAPI.HandleRequest.Handler.Validator;
using FluentValidation.AspNetCore;
using Trading.Exception;
using System.Reflection;

namespace TradingAPI
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

            services.Configure<AppSetting>(Configuration.GetSection("ApplicationSettings"));

            services.AddDbContext<TradingContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")), ServiceLifetime.Transient);

           

            services.AddAppMVC();

            //Resiter handler to handle fluent validations
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAppSwaggerGen();
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfile>(), typeof(Startup));


        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //To register Autofac module
         
                
            builder.RegisterModule(new AutofacModule());
            //builder.RegisterModule(new MediatorModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
              //  app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            
            app.UseMiddleware<ExceptionMiddleware>();
          
            app.UseAuthentication();


            app.UseHttpsRedirection();

            app.UseAppSwagger();

            app.UseRouting();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
