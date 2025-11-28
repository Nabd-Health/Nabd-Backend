using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Nabd.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        // دالة لتسجيل خدمات الـ JWT والـ Identity
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // 1. إعدادات قراءة التوكن (JWT Bearer)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // يجب أن نتحقق من أن التوكن موقع بنفس مفتاحنا السري
                        ValidateIssuerSigningKey = true,
                        // قراءة المفتاح من appsettings.json
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        // التحقق من مصدر التوكن (سيرفرنا)
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        // لا نحتاج لـ Audience في هذا المشروع
                        ValidateAudience = false
                    };
                });

            // 2. إعدادات الـ Identity (مثل صلاحيات الباسورد)
            // (سنتجاهل الـ AddRoles والـ DbContext حالياً للتركيز على JWT، وسنضيفها لاحقاً في خطوة الـ Seeding)
            services.AddAuthorization();

            return services;
        }
    }
}