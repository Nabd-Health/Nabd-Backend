using Nabd.Core.Enums.Operations; 
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    public class BookAppointmentRequest
    {
        // ==========================================
        // 1. The Parties
        // ==========================================

        [Required(ErrorMessage = "يجب تحديد الطبيب.")]
        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "يجب تحديد الفرع/العيادة.")]
        public Guid ClinicBranchId { get; set; }


        public Guid? PatientId { get; set; }

        // ==========================================
        // 2. Timing
        // ==========================================

        [Required(ErrorMessage = "تاريخ ووقت الموعد مطلوب.")]
        public DateTime AppointmentDate { get; set; } 

        // ==========================================
        // 3. Context 
        // ==========================================

        [Required(ErrorMessage = "نوع الكشف مطلوب.")]
        public AppointmentType Type { get; set; } 

        [MaxLength(200, ErrorMessage = "سبب الزيارة لا يجب أن يتجاوز 200 حرف.")]
        public string? ReasonForVisit { get; set; } 

        // ==========================================
        // 4. Payment 
        // ==========================================

        public bool IsPaid { get; set; } = false;
        public string? PaymentTransactionId { get; set; }
    }
}