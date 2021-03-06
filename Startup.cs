﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace ApiWebApp
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
            services.AddMvcCore(
                    options =>
                    {
                        options.RespectBrowserAcceptHeader = true;
                        options.ReturnHttpNotAcceptable = true;
                    }
                )
                .AddAuthorization()
                .AddXmlDataContractSerializerFormatters()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                {
                    if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                    {
                        resolver.NamingStrategy = null;
                    }
                });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["identityServer"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
                    options.TokenValidationParameters.RequireExpirationTime = true;
                    options.Audience = "ArticlesApi";
                });
            services.AddAuthenticationCore(options =>
            {
                options.AddScheme<SimpleAuthenticationHandler>("SimpleScheme", "demo scheme");
            });
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:8000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
