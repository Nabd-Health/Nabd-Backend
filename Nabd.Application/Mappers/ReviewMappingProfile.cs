using AutoMapper;
using Nabd.Application.DTOs.Feedback; 
using Nabd.Core.Entities.Feedback;   

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
             
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.IsAnonymous ? "فاعل خير" : (src.Patient != null ? src.Patient.FullName : "مستخدم")))

              
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