using Nabd.Core.Enums.Medical; 
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Medical
{
    public class CreateMedicalHistoryItemRequest
    {

        [Required(ErrorMessage = "رقم ملف المريض مطلوب.")]
        public Guid PatientId { get; set; }

        
        [Required(ErrorMessage = "نوع السجل الطبي مطلوب.")]
        public HistoryEventType Type { get; set; } 

     
        [Required(ErrorMessage = "العنوان مطلوب.")]
        [MaxLength(150, ErrorMessage = "العنوان لا يجب أن يتجاوز 150 حرف.")]
        public required string Title { get; set; } 
        [MaxLength(500, ErrorMessage = "التفاصيل لا يجب أن تتجاوز 500 حرف.")]
        public string? Details { get; set; }

      [Required(ErrorMessage = "تاريخ الحدوث مطلوب.")]
        public DateTime EventDate { get; set; } 


        public bool IsCritical { get; set; } = false; 

    }
}