using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Nabd.Application.Extensions
{
    // يضيف جميع قواعد FluentValidation إلى حاوية الخدمات (DI Container)
    public static class ValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            // تسجيل جميع الـ Validators الموجودة في هذا Assembly (Nabd.Application)
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure validation behavior (إيقاف التحقق عند أول خطأ)
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

            return services;
        }
    }
}