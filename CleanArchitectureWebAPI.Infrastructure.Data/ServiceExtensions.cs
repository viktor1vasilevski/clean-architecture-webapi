using CleanArchitectureWebAPI.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Infrastructure.Data
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services)
        {
            // Setting Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>()
                .AddDefaultTokenProviders();

            // Setting JWT Token
            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-my-secret-key"));
            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = singingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(x => x.DefaultAuthenticateScheme = JwtBearerDefaults
                    .AuthenticationScheme)
                    .AddJwtBearer(jwt =>
                    {
                        jwt.TokenValidationParameters = tokenValidationParameters;
                    });
        }
    }
}
