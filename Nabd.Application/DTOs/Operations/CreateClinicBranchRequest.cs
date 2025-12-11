using Nabd.Core.Enums; 
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    public class CreateClinicBranchRequest
    {
        // ==========================================
        // 1. Basic Details
        // ==========================================

        [Required(ErrorMessage = "اسم العيادة/الفرع مطلوب.")]
        [MaxLength(100, ErrorMessage = "الاسم لا يجب أن يتجاوز 100 حرف.")]
        public required string Name { get; set; } 

        [Required(ErrorMessage = "رقم الهاتف مطلوب للتواصل.")]
        [Phone(ErrorMessage = "صيغة رقم الهاتف غير صحيحة.")]
        public required string PhoneNumber { get; set; }

        [Phone]
        public string? LandlineNumber { get; set; }

        // ==========================================
        // 2. Location 
        // ==========================================

        [Required(ErrorMessage = "المحافظة مطلوبة.")]
        public Governorate Governorate { get; set; } 

        [Required(ErrorMessage = "المدينة/الحي مطلوب.")]
        [MaxLength(50)]
        public required string City { get; set; } 

        [Required(ErrorMessage = "العنوان التفصيلي مطلوب.")]
        [MaxLength(200)]
        public required string StreetAddress { get; set; } 


        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 3. Financials 
        // ==========================================

     
        [Range(0, 10000, ErrorMessage = "السعر يجب أن يكون قيمة منطقية.")]
        public decimal? CustomConsultationFee { get; set; }
    }
}