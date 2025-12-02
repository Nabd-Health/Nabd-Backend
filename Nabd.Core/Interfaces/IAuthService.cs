using System;
using System.Threading.Tasks;
using Nabd.Core.Entities;

namespace Nabd.Core.Interfaces
{
    // هذا العقد هو بوابة الدخول وإدارة حسابات المستخدمين (الأطباء والمرضى)
    public interface IAuthService
    {
        // =========================================================
        // 1. التسجيل (Registration) - مفصولة حسب الدور
        // =========================================================

        /// <summary>
        /// تسجيل حساب طبيب جديد.
        /// </summary>
        /// <param name="doctorDto">بيانات التسجيل (RegisterDoctorDto).</param>
        /// <returns>AuthResponseDto مع مفاتيح التوكن.</returns>
        // (سنعتمد على الـ Implementation في Application layer لتحويل الـ object إلى DTO)
        Task<object> RegisterDoctorAsync(object doctorDto);

        /// <summary>
        /// تسجيل حساب مريض جديد.
        /// </summary>
        /// <param name="patientDto">بيانات التسجيل (RegisterPatientDto).</param>
        /// <returns>AuthResponseDto مع مفاتيح التوكن.</returns>
        Task<object> RegisterPatientAsync(object patientDto);

        // =========================================================
        // 2. الدخول (Login) - Core Authentication
        // =========================================================

        /// <summary>
        /// تسجيل الدخول والتحقق من المستخدم.
        /// </summary>
        /// <param name="loginDto">بيانات الدخول (Email, Password).</param>
        /// <returns>AuthResponseDto مع التوكن.</returns>
        Task<object> LoginAsync(object loginDto);

        // =========================================================
        // 3. إدارة التوكن والأمان (Token Management)
        // =========================================================

        /// <summary>
        /// لتجديد صلاحية الجلسة (Token Rotation).
        /// </summary>
        Task<object> RenewTokenAsync(string refreshToken);

        /// <summary>
        /// للخروج الآمن (Revoke all sessions).
        /// </summary>
        Task<bool> RevokeTokenAsync(string token); // Revoke RefreshToken for logout

        // =========================================================
        // 4. إدارة الملف الشخصي (Profile Management) - Settings
        // =========================================================

        /// <summary>
        /// جلب الملف الشخصي الكامل للطبيب.
        /// </summary>
        Task<object> GetDoctorProfileAsync(Guid userId);

        /// <summary>
        /// جلب الملف الشخصي الكامل للمريض.
        /// </summary>
        Task<object> GetPatientProfileAsync(Guid userId);

        /// <summary>
        /// تحديث الملف الشخصي العام (يستخدم لصفحة Settings الموحدة).
        /// </summary>
        /// <param name="updateDto">UpdateDto (UpdateDoctorProfileDto or UpdatePatientProfileDto).</param>
        Task<bool> UpdateProfileAsync(Guid userId, object updateDto);

        // =========================================================
        // 5. التحقق (Verification) - Future Proofing
        // =========================================================

        /// <summary>
        /// لإعادة إرسال كود التفعيل للإيميل.
        /// </summary>
        Task<bool> ResendEmailVerificationAsync(Guid userId);
    }
}