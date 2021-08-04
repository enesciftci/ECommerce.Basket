using ECommerce.Basket.Api.Filters;
using ECommerce.Basket.Api.Infrastructure;
using ECommerce.Basket.Business.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

namespace ECommerce.Basket.Api
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
            services.AddAuthentication(Configuration);
            services.AddControllers(opt => opt.Filters.Add(new ValidationFilter()))
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                    fv.DisableDataAnnotationsValidation = false;
                });
            services.AddApplicationServices(Configuration);
            services.AddAutoMapper();
            services.AddApiVersioning();
            services.AddLogging();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseApiVersioning();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
