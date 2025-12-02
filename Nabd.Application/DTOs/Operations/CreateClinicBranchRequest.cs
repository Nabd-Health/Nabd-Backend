using Nabd.Core.Enums; // Governorate
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    public class CreateClinicBranchRequest
    {
        // ==========================================
        // 1. Basic Details (بيانات الفرع)
        // ==========================================

        [Required(ErrorMessage = "اسم العيادة/الفرع مطلوب.")]
        [MaxLength(100, ErrorMessage = "الاسم لا يجب أن يتجاوز 100 حرف.")]
        public required string Name { get; set; } // مثال: "عيادة المعادي"

        [Required(ErrorMessage = "رقم الهاتف مطلوب للتواصل.")]
        [Phone(ErrorMessage = "صيغة رقم الهاتف غير صحيحة.")]
        public required string PhoneNumber { get; set; }

        [Phone]
        public string? LandlineNumber { get; set; }

        // ==========================================
        // 2. Location (الموقع - تم دمجه هنا)
        // ==========================================

        [Required(ErrorMessage = "المحافظة مطلوبة.")]
        public Governorate Governorate { get; set; } // Enum (Cairo, Giza, Sharqia...)

        [Required(ErrorMessage = "المدينة/الحي مطلوب.")]
        [MaxLength(50)]
        public required string City { get; set; } // مثال: "الزقازيق - القومية"

        [Required(ErrorMessage = "العنوان التفصيلي مطلوب.")]
        [MaxLength(200)]
        public required string StreetAddress { get; set; } // مثال: "شارع المحافظة، برج الأطباء، الدور الثالث"

        // (بيانات الخريطة - اختيارية ولكن مفيدة جداً للمريض)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 3. Financials (السعر الخاص بالفرع)
        // ==========================================

        // لو null، السيستم هيستخدم سعر الدكتور الافتراضي (Default Fee)
        [Range(0, 10000, ErrorMessage = "السعر يجب أن يكون قيمة منطقية.")]
        public decimal? CustomConsultationFee { get; set; }
    }
}