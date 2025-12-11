using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Nabd.Application.Extensions
{

    public static class ValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
           
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

  
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

            return services;
        }
    }
}