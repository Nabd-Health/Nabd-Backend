using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nabd.Shared.Configurations; // استخدام الـ Namespace الجديد

namespace Nabd.Shared.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. ربط إعدادات JWT من ملف appsettings.json
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
            {
                throw new InvalidOperationException("إعدادات JWT غير موجودة أو غير صالحة. يرجى التحقق من appsettings.json");
            }

            // تسجيل الإعدادات للـ Dependency Injection
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            // 2. إعداد خدمة المصادقة (Authentication)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // يفضل true في الإنتاج (Production)
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkewMinutes)
                };

                // 3. أحداث مخصصة للتشخيص (Logging & Debugging)
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        // ==========================================
        // Authorization Policies (تحديد الصلاحيات)
        // ==========================================
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // السياسات الأساسية لـ "نبض"
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
                options.AddPolicy("VerifierOnly", policy => policy.RequireRole("Verifier"));

                // السياسات المركبة (Combined Policies)
                options.AddPolicy("AdminOrVerifier", policy => policy.RequireRole("Admin", "Verifier"));

                // [تم الحذف]: LaboratoryOnly, PharmacyOnly, HealthcareProvider
            });

            return services;
        }
    }
}