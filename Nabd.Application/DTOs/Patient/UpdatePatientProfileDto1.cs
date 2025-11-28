using Nabd.Application.DTOs.Patient;
using Nabd.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Application.DTOs.Patient
{
  
        // هذا DTO هو مدخل البيانات (Input) لعمليات التحديث الجزئي لملف المريض (Settings Page)
        public class UpdatePatientProfileDto1
    {
            // ==================================================
            // 1. Personal Info (Demographics) - كلها قابلة للـ null
            // ==================================================

            [MaxLength(100)]
            public string? FullName { get; set; }

            [Phone(ErrorMessage = "Invalid phone number format.")]
            public string? PhoneNumber { get; set; }

            public Gender? Gender { get; set; } // Enum? (قيمة اختيارية)

            public string? Address { get; set; }
            public string? City { get; set; }
            public string? JobTitle { get; set; }
            public string? MaritalStatus { get; set; }

            // ==================================================
            // 2. Medical Baseline & Vitals
            // ==================================================

            public BloodType? BloodType { get; set; }

            // Vitals: نستخدم double? لضمان المرونة في التحديثات الجزئية
            [Range(1.0, 300.0)]
            public double? Height { get; set; }

            [Range(1.0, 500.0)]
            public double? Weight { get; set; }

            [MaxLength(500)]
            public string? ChronicDiseases { get; set; }

            [MaxLength(500)]
            public string? Allergies { get; set; } // حقل أمان مهم

            // ==================================================
            // 3. Emergency Contact
            // ==================================================

            [MaxLength(100)]
            public string? EmergencyContactName { get; set; }

            [Phone]
            public string? EmergencyContactPhone { get; set; }
        }
    }

