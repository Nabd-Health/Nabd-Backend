using Nabd.Core.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces
{
    // هذا العقد مسؤول عن توليد وإدارة التوكنات (مفاتيح الدخول)
    public interface ITokenService
    {
        // =========================================================
        // 1. توليد التوكن الرئيسي (JWT)
        // =========================================================
        
        /// <summary>
        /// يُنشئ توكن دخول مشفر للمستخدم المعين.
        /// </summary>
        /// <param name="user">بيانات المستخدم (AppUser).</param>
        /// <returns>سلسلة نصية تحتوي على التوكن (JWT).</returns>
        string CreateToken(AppUser user);

        // =========================================================
        // 2. توليد وإدارة Refresh Token (للبقاء مسجل دخول)
        // =========================================================
        
        /// <summary>
        /// يُنشئ توكن جديد لتجديد صلاحية الجلسة آليًا.
        /// </summary>
        /// <returns>كائن RefreshToken جديد.</returns>
       
        Task<RefreshToken> GenerateRefreshToken(Guid appUserId, string ipAddress);

        /// <summary>
        /// يُبطل توكن التجديد (RefreshToken) الحالي ويُسجل توكنًا بديلاً له.
        /// </summary>
        /// <param name="token">توكن التجديد المراد إبطاله.</param>
        /// <param name="ipAddress">عنوان IP الذي تم منه الإبطال.</param>
        /// <param name="reason">سبب الإبطال (مثال: تسجيل خروج، سرقة).</param>
        /// <returns>توكن تجديد جديد لاستخدامه في الطلب التالي.</returns>
        Task<RefreshToken> ReplaceRefreshToken(string token, string ipAddress, string reason);
        
        /// <summary>
        /// يجدد توكن الدخول بناءً على توكن التجديد (Refresh Token) القديم.
        /// </summary>
        /// <param name="refreshToken">توكن التجديد.</param>
        /// <returns>كائن AuthResponse يحتوي على JWT جديد و Refresh Token جديد.</returns>
        Task<object> RenewTokenAsync(string refreshToken);

        // =========================================================
        // 3. تحليل التوكن (For Future Use)
        // =========================================================
        
        /// <summary>
        /// يستخرج المطالبات (Claims) من التوكن المشفر (JWT).
        /// </summary>
        /// <param name="token">التوكن المراد تحليله.</param>
        /// <returns>قائمة المطالبات (User ID, Role, etc.).</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}