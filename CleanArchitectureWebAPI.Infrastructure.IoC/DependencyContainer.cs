﻿using CleanArchitectureWebAPI.Application.Interfaces;
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
            services.AddScoped<ISoapService, SoapService>();
            services.AddScoped<ISoapRepository, SoapRepository>();

            services.AddScoped<IOilService, OilService>();
            services.AddScoped<IOilRepository, OilRepository>();

            services.AddScoped<IBalmService, BalmService>();
            services.AddScoped<IBalmRepository, BalmRepository>();
        }
    }
}
