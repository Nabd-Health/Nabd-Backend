using AutoMapper;
using Nabd.Application.DTOs.Identity;
using Nabd.Core.DTOs; // تأكد إن ده المسار الصح لـ AuthResponseDto
using Nabd.Core.Entities.Identity; // AppUser
using Nabd.Core.Entities.Profiles; // Doctor, Patient
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity; // UserType, DoctorStatus

namespace Nabd.Application.Mappers
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            // ==========================================
            // I. Registration Mappings (DTO -> Entity)
            // ==========================================

            // 1. RegisterDoctorDto => AppUser
            CreateMap<RegisterDoctorDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Doctor))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true));

            // 2. RegisterPatientDto => AppUser
            CreateMap<RegisterPatientDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Patient))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true));

            // 3. RegisterDoctorDto => Doctor
            CreateMap<RegisterDoctorDto, Doctor>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => DoctorStatus.Pending))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // 4. RegisterPatientDto => Patient
            CreateMap<RegisterPatientDto, Patient>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.Ignore());

            // ==========================================
            // II. Authentication Response Mappings (Entity -> DTO)
            // ==========================================

            // 5. AppUser => AuthResponseDto
            CreateMap<AppUser, AuthResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                // ✅ التعديل: UserRole بقت UserType
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                // ✅ التعديل: IsAuthSuccessful بقت IsSuccess
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore());

            // 6. RefreshToken => AuthResponseDto
            CreateMap<RefreshToken, AuthResponseDto>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                // ✅ التعديل: IsAuthSuccessful بقت IsSuccess
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => true));
        }
    }
}