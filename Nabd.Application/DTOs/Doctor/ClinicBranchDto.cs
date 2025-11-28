using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Doctor
{
    // هذا DTO يمثل فرع عيادة الطبيب (يستخدم لإضافة فروع جديدة وعرضها على الخريطة)
    public class ClinicBranchDto
    {
        // ==================================================
        // 1. Identification & Core Info
        // ==================================================

        public Guid? Id { get; set; } // Id للفرع (Nullable لو بننشئ فرع جديد)

        [Required(ErrorMessage = "Branch name is required.")]
        [MaxLength(100)]
        public required string Name { get; set; } // مثال: "فرع القومية"

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(250)]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string PhoneNumber { get; set; }

        // ==================================================
        // 2. Geolocation (Crucial for Find Doctor map view)
        // ==================================================

        // إحداثيات الموقع (مهمة للـ API اللي بيحسب أقرب دكتور)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [MaxLength(300)]
        public string? GoogleMapLink { get; set; }

        // ==================================================
        // 3. Financials & Operational Status
        // ==================================================

        // سعر كشف مخصص لهذا الفرع (لتجاوز السعر الأساسي للدكتور)
        [Range(0, 10000, ErrorMessage = "Fee must be a valid amount.")]
        public decimal? CustomConsultationFee { get; set; }

        // حالة الفرع (هل يعمل؟)
        public bool IsActive { get; set; } = true;

        // ==================================================
        // 4. Scheduling Context (Future Detail)
        // ==================================================

        // List of DTOs representing the schedules/shifts for this specific branch
        // public List<ScheduleDto>? Schedules { get; set; } 
    }
}