using Nabd.Core.Entities.Profiles; // لاستخدام Entity Doctor
using Nabd.Core.Enums; // لاستخدام Governorate
using Nabd.Core.Enums.Medical; // لاستخدام MedicalSpecialty
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    // هذا الريبوزيتوري مسؤول عن إدارة عمليات استرجاع وبيانات الأطباء
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // ==========================================
        // I. Get By ID with Eager Loading (لتحميل العلاقات مرة واحدة)
        // ==========================================

        // 1. استرجاع الطبيب مع كل البيانات المطلوبة في صفحة البروفايل (Clinic, Schedules, Documents)
        Task<Doctor?> GetByIdWithDetailsAsync(Guid id);

        // 2. استرجاع الطبيب مع بيانات العيادات/الفروع المرتبطة به
        // (نغيرها من Clinic إلى ClinicBranch في التفيذ)
        Task<Doctor?> GetByIdWithBranchesAsync(Guid id);

        // 3. استرجاع الطبيب مع مواعيد عمله المتاحة
        // (سنستخدمها لـ Booking Engine)
        Task<Doctor?> GetByIdWithSchedulesAsync(Guid id); // تم تغيير Availabilities إلى Schedules

        // ==========================================
        // II. Core Retrieval (الاسترجاع الأساسي)
        // ==========================================

        // 4. البحث عن الطبيب بالبريد الإلكتروني (لتسجيل الدخول / الأمان)
        Task<Doctor?> GetByEmailAsync(string email);

        // 5. استرجاع جميع الأطباء الموثقين فقط
        Task<IEnumerable<Doctor>> GetVerifiedDoctorsAsync();

        // 6. جلب الأطباء حسب التخصص (MedicalSpecialty)
        Task<IEnumerable<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty);

        // 7. جلب الأطباء حسب المحافظة (للتنظيم الجغرافي)
        Task<IEnumerable<Doctor>> GetDoctorsByGovernorateAsync(Governorate governorate);

        // ==========================================
        // III. Search & Filtering (مُحرك البحث المتقدم)
        // ==========================================

        // 8. محرك البحث العام الذي يدعم كل معايير التصفية في الـ UI
        Task<IEnumerable<Doctor>> SearchDoctorsAsync(
            string? searchTerm = null,
            MedicalSpecialty? specialty = null,
            Governorate? governorate = null,
            int? minYearsOfExperience = null,
            decimal? maxConsultationFee = null,
            double? minRating = null
        );

        // 9. التحقق من توافر الطبيب في تاريخ ووقت محدد (مهم لـ Booking Engine)
        Task<bool> IsAvailableAtAsync(Guid doctorId, DateTime dateTime);

        // 10. جلب الدكاترة الموثقين مع كل البيانات المطلوبة للـ list (optimized for performance)
        // هذا الميثود ممتاز لأنه يحدد بالضبط ما يجب تحميله لصفحة القائمة الرئيسية.
        Task<IEnumerable<Doctor>> GetVerifiedDoctorsWithDetailsForListAsync();
    }
}