using AutoMapper;
using Nabd.Application.DTOs.Feedback; // DTOs (DoctorReviewResponse, Create/Update Request)
using Nabd.Core.Entities.Feedback;    // DoctorReview Entity

namespace Nabd.Application.Mappers
{
    // هذا الملف مسؤول عن تحويل بيانات التقييمات بين الـ API والـ Database
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            // ==========================================
            // I. Doctor Review Mappings (تقييم الطبيب)
            // ==========================================

            // 1. Entity -> Response DTO
            CreateMap<DoctorReview, DoctorReviewResponse>()
                // [منطق الخصوصية]: إذا اختار المريض إخفاء هويته، نعرض اسماً مستعاراً
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.IsAnonymous ? "فاعل خير" : (src.Patient != null ? src.Patient.FullName : "مستخدم")))

                // [منطق الخصوصية + Flattening]: إخفاء الصورة لو مجهول، أو جلبها من AppUser لو معلوم
                .ForMember(dest => dest.PatientProfileImageUrl, opt => opt.MapFrom(src =>
                    src.IsAnonymous ? null :
                    (src.Patient != null && src.Patient.AppUser != null ? src.Patient.AppUser.ProfilePictureUrl : null)));

            // 2. Create Request -> Entity
            CreateMap<CreateDoctorReviewRequest, DoctorReview>();

            // 3. Update Request -> Entity (Patch Update)
            CreateMap<UpdateDoctorReviewRequest, DoctorReview>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}