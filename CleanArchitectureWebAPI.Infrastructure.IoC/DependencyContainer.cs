using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.Services;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Infrastructure.Data.Context;
using CleanArchitectureWebAPI.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void AddIoCService(this IServiceCollection services)
        {
            // IoC - Inversion Of Control
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
