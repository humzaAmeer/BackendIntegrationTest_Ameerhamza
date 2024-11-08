// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using BackendIntegrationTest_Ameerhamza.Configuration;
using BackendIntegrationTest_Ameerhamza.Services.TodoList;
using BackendIntegrationTest_Ameerhamza.Services.WeatherService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BackendIntegrationTest_Ameerhamza
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets Configuration Object
        /// </summary>
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// Configure Services function
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var con = this.Configuration.GetConnectionString("DefaltConnection");
            // DB configuration
            services.AddDbContext<TodoDbContext>(options =>

           options.UseSqlServer(this.Configuration.GetConnectionString("DefaltConnection")));
            
            
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IWeatherService, WeatherService>();

            // Swagger Settings
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Test REST Api", Version = "V1" });
            });
     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// Coonfigure Function
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Env</param>
        /// <param name="factory">factory</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceScopeFactory factory)
        {

            app.UseExceptionHandler("/error");

            // Swagger Settings
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/Swagger/v1/Swagger.json", name: "Test REST Api");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
