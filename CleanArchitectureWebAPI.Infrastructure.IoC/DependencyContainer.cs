using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.Services;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterIoCServices(this IServiceCollection services)
        {
            /*  
                The principle is Dependency Inversion, but the way we 
                implement the principle is Dependency Injection.

                Here is where we implement the Dependency Injection.
                It's how the layers communicate each other.
            */

            // Application
            services.AddScoped<ISoapService, SoapService>();
            services.AddScoped<IOilService, OilService>();
            services.AddScoped<IBalmService, BalmService>();

            // Domain.Interfaces > Infrastructure.Data.Repositories
            services.AddScoped<ISoapRepository, SoapRepository>();
            services.AddScoped<IOilRepository, OilRepository>();
            services.AddScoped<IBalmRepository, BalmRepository>();
        }
    }
}
