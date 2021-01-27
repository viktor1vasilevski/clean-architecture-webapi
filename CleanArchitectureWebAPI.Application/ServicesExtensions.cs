using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CleanArchitectureWebAPI.Application
{
    public static class ServicesExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // With this we say to the services, use the AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // we can add other services here
        }
    }
}
