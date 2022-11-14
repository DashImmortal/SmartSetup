using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using SmartDevice.Services.RoomService;
using SmartDevice.Services.SmartDeviceService;

namespace SmartDevice
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartDevice", Version = "v1" });
            });
            services.AddScoped<ISmartDeviceService, SmartDeviceService>(); 
            services.AddScoped<IRoomService, RoomService>(); 
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
            services.AddDbContext<SmartDeviceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SmartSetupDB")));

            services.AddDbContext<RoomsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SmartSetupDB")));

            //services.AddDbContext<SmartDeviceContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("SmartDeviceContext")));
            //
            //services.AddDbContext<RoomsContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("RoomsContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartDevice v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
