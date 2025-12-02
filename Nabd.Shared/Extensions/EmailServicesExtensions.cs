using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Shared.Configurations; // Namespace الجديد

namespace Nabd.Shared.Extensions
{
    public static class EmailServicesExtensions // تم تصحيح Spelling كلمة Extensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            // قراءة الإعدادات من appsettings.json
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

            if (emailSettings == null)
            {
                throw new InvalidOperationException("EmailSettings are not configured properly.");
            }

            // تسجيل خدمة FluentEmail مع SMTP Client
            services.AddFluentEmail(emailSettings.FromEmail, emailSettings.FromName)
                .AddSmtpSender(new SmtpClient(emailSettings.SmtpHost)
                {
                    Port = emailSettings.SmtpPort,
                    Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword),
                    EnableSsl = emailSettings.EnableSsl
                });

            return services;
        }
    }
}