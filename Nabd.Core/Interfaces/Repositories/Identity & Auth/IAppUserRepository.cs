using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Identity
{
    // هذا الريبوزيتوري مسؤول عن إدارة المستخدمين المشتركين في النظام (AppUser)
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        // ==========================================
        // I. Authentication & Identity
        // ==========================================

        // 1. استرجاع المستخدم بواسطة البريد الإلكتروني (لتسجيل الدخول)
        Task<AppUser?> GetByEmailAsync(string email);

        // 2. التحقق من وجود اسم مستخدم أو بريد إلكتروني قبل التسجيل
        Task<bool> IsUserNameOrEmailTakenAsync(string userName, string email);

        // 3. جلب المستخدم مع كل التوكنات الخاصة به (مهم للـ Token Rotation)
        Task<AppUser?> GetByIdWithRefreshTokensAsync(Guid userId);

        // 4. جلب المستخدم مع الـ Profile الخاص به (Doctor/Patient)
        Task<AppUser?> GetByIdWithProfileAsync(Guid userId);

        // ==========================================
        // II. System Management & Roles
        // ==========================================

        // 5. جلب المستخدمين حسب نوع دورهم (Doctor, Patient, Verifier, etc.)
        Task<IEnumerable<AppUser>> GetUsersByTypeAsync(UserType userType);

        // 6. التحقق من صحة Security Stamp (مهم لإلغاء الجلسات القديمة بعد تغيير الباسورد)
        Task<string?> GetSecurityStampAsync(Guid userId);

        // 7. جلب جميع الحسابات المعلقة التفعيل (لإدارة النظام)
        Task<IEnumerable<AppUser>> GetPendingVerificationUsersAsync();
    }
}