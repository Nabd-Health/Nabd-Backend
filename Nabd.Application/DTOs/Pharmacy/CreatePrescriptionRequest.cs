using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Pharmacy
{
    public class CreatePrescriptionRequest
    {
        [Required(ErrorMessage = "رقم الموعد (AppointmentId) مطلوب.")]
        public Guid AppointmentId { get; set; }

        public Guid? ConsultationRecordId { get; set; }

        [JsonIgnore]
        public Guid DoctorId { get; set; }

        [MaxLength(500, ErrorMessage = "الملاحظات لا يجب أن تتجاوز 500 حرف.")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "يجب إضافة دواء واحد على الأقل.")]
        [MinLength(1, ErrorMessage = "الروشتة فارغة.")]

        public List<CreatePrescriptionItemDto> Items { get; set; } = new();
    }

   
    public class CreatePrescriptionItemDto
    {
        [Required(ErrorMessage = "يجب اختيار الدواء.")]
        public Guid MedicationId { get; set; }

        [Required(ErrorMessage = "الجرعة مطلوبة.")]
        [MaxLength(50)]
        public string Dosage { get; set; } = string.Empty;

        [Required(ErrorMessage = "التكرار مطلوب.")]
        [MaxLength(100)]
        public string Frequency { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Duration { get; set; }

        [MaxLength(200)]
        public string? Instructions { get; set; }
    }
}