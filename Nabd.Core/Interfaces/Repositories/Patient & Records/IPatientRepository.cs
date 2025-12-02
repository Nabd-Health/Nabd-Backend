using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Interfaces.Repositories.Base;
using Nabd.Core.DTOs; // ✅ استخدام الـ DTO من داخل الـ Core

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
    // هذا الريبوزيتوري مسؤول عن إدارة عمليات استرجاع وبيانات المرضى
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // ==========================================
        // I. Core Retrieval (الاسترجاع الأساسي)
        // ==========================================

        // 1. استرجاع المريض مع كل البيانات المطلوبة في صفحة البروفايل (MedicalHistory, Attachments, Reviews)
        Task<Patient?> GetByIdWithDetailsAsync(Guid id);

        // 2. البحث عن المريض بالبريد الإلكتروني (لتسجيل الدخول / الأمان)
        Task<Patient?> GetByEmailAsync(string email);

        // 3. جلب جميع المرضى مع ملفاتهم الطبية (مهم لتجهيز البيانات لـ AI Training)
        Task<IEnumerable<Patient>> GetPatientsWithMedicalHistoryAsync();

        // ==========================================
        // II. Specialized & Optimized Queries
        // ==========================================

        // 4. جلب قائمة المرضى الخاصين بطبيب معين (مع Pagination والأداء الأمثل)
        // (مهم لـ Doctor Dashboard لكي يرى مرضاه فقط)
        // ✅ تم استخدام DTO موجود في Core لضمان صحة المعمارية
        Task<(IEnumerable<DoctorPatientDto> Patients, int TotalCount)> GetDoctorPatientsOptimizedAsync(
            Guid doctorId,
            int pageNumber,
            int pageSize);

        /// <summary>
        /// جلب المريض مع الموقع الجغرافي (للبحث عن أي شيء قريب منه)
        /// </summary>
        Task<Patient?> GetPatientWithLocationAsync(Guid patientId); // تم تغيير Address إلى Location ليعكس طبيعة البيانات الحديثة

        // ==========================================
        // III. Write Operations (العمليات الكتابية)
        // ==========================================

        // 5. الحذف (Soft Delete): تحديد ما إذا كان الحذف سيكون مؤقتًا أو نهائيًا
        // (مهم جدًا للحفاظ على السجل الطبي)
        Task RemoveAsync(Patient patient, bool softDelete = true);
    }
}