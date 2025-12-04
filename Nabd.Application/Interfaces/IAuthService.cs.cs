using Nabd.Application.DTOs.Identity; // لاستخدام الـ DTOs
using Nabd.Core.DTOs;
using System.Threading.Tasks;

namespace Nabd.Application.Interfaces
{
    // هذه الواجهة تحدد عقود عمليات المصادقة والتسجيل
    public interface IAuthService
    {
        // 1. تسجيل الدخول
        Task<AuthResponseDto> LoginAsync(LoginDto model);

        // 2. تسجيل طبيب جديد (مع العيادة)
        Task<AuthResponseDto> RegisterDoctorAsync(RegisterDoctorDto model);

        // 3. تسجيل مريض جديد (مع الملف الطبي الأولي)
        Task<AuthResponseDto> RegisterPatientAsync(RegisterPatientDto model);

        // 4. تجديد التوكن (Token Rotation)
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest model);

        // 5. إبطال التوكن (تسجيل الخروج)
        Task<bool> RevokeTokenAsync(string token);

        // 6. تفعيل الإيميل (اختياري للمستقبل)
        Task VerifyEmailAsync(VerifyEmailRequest model);
    }
}