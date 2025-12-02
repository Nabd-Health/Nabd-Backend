using Nabd.Core.Enums.Operations; // AppointmentType
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    public class BookAppointmentRequest
    {
        // ==========================================
        // 1. The Parties (الأطراف)
        // ==========================================

        [Required(ErrorMessage = "يجب تحديد الطبيب.")]
        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "يجب تحديد الفرع/العيادة.")]
        public Guid ClinicBranchId { get; set; }

        // (PatientId غالباً بنجيبه من التوكن، بس بنحطه هنا عشان لو Admin بيحجز لمريض)
        public Guid? PatientId { get; set; }

        // ==========================================
        // 2. Timing (الزمان)
        // ==========================================

        [Required(ErrorMessage = "تاريخ ووقت الموعد مطلوب.")]
        public DateTime AppointmentDate { get; set; } // يجب أن يكون DateTime (تاريخ + ساعة)

        // ==========================================
        // 3. Context (السياق - مهم للـ Doctor Dashboard)
        // ==========================================

        [Required(ErrorMessage = "نوع الكشف مطلوب.")]
        public AppointmentType Type { get; set; } // (Regular, FollowUp)

        [MaxLength(200, ErrorMessage = "سبب الزيارة لا يجب أن يتجاوز 200 حرف.")]
        public string? ReasonForVisit { get; set; } // "صداع مستمر"، "متابعة تحليل"

        // ==========================================
        // 4. Payment (مؤشر للدفع)
        // ==========================================
        // هل تم الدفع أونلاين؟ (لو السيستم فيه دفع مسبق)
        public bool IsPaid { get; set; } = false;
        public string? PaymentTransactionId { get; set; }
    }
}