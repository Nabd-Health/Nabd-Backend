using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Application.Services.AI;    
using Nabd.Application.Services.Identity;
using Nabd.Core.Interfaces;            
using Nabd.Infrastructure.Data;          
using Nabd.Infrastructure.Repositories;   
using System;
using AutoMapper;
using Nabd.Application.Interfaces;



namespace Nabd.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // ==========================================
            // 1. Database Connection 
            // ==========================================
            services.AddDbContext<NabdDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // ==========================================
            // 2. Data Access Layer 
            // ==========================================
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ==========================================
            // 3. Application Services 
            // ==========================================


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

    
            services.AddScoped<IAIService, MockAIService>();

            // ==========================================
            // 4. Tools 
            // ==========================================
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}