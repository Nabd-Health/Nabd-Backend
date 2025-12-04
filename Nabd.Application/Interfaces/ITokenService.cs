using Nabd.Core.Entities.Identity; // AppUser, RefreshToken
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nabd.Application.Interfaces
{
    public interface ITokenService
    {
        // 1. إنشاء توكن الدخول (JWT Access Token)
        // يحتوي على الـ Claims (الاسم، الإيميل، الدور) وصلاحيته قصيرة (مثلاً 15 دقيقة)
        string CreateToken(AppUser user);

        // 2. إنشاء توكن التجديد (Refresh Token)
        // يستخدم للحصول على Access Token جديد دون الحاجة لتسجيل الدخول مرة أخرى
        // يتم حفظه في الداتابيز لغرض الأمان (Revocation)
        Task<RefreshToken> GenerateRefreshToken(Guid userId, string ipAddress);

        // 3. استخراج بيانات المستخدم من التوكن المنتهي
        // (نحتاجها عند طلب تجديد التوكن للتأكد من هوية صاحبه)
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}