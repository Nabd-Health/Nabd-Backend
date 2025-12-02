
/*



using AutoMapper;
using Nabd.Application.DTOs.Feedback; // DTOs
using Nabd.Core.Entities.Feedback;    // Entity

namespace Nabd.Application.Mappers
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            // ==========================================
            // I. Doctor Review Mappings
            // ==========================================

            // 1. Entity -> Response DTO
            CreateMap<DoctorReview, DoctorReviewResponse>()
                // [تصحيح]: التعامل مع الاسم (موجود في Patient مباشرة)
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.IsAnonymous ? "فاعل خير" : (src.Patient != null ? src.Patient.FullName : "مستخدم محذوف")))

                // [تصحيح هام]: الصورة موجودة داخل AppUser وليس Patient مباشرة
                .ForMember(dest => dest.PatientProfileImageUrl, opt => opt.MapFrom(src =>
                    src.IsAnonymous ? null :
                   // (src.Patient != null && src.Patient.AppUser != null ? src.Patient.AppUser.ProfileImageUrl : null)));

            // 2. Request -> Entity
            CreateMap<CreateDoctorReviewRequest, DoctorReview>();

            // 3. Update Request -> Entity (Patch)
            CreateMap<UpdateDoctorReviewRequest, DoctorReview>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}



*/