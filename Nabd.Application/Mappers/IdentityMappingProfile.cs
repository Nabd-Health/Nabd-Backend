using AutoMapper;
using Nabd.Application.DTOs.Identity;
using Nabd.Core.Entities.Identity; // AppUser, RefreshToken
using Nabd.Core.Entities.Profiles; // Doctor, Patient
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity; // UserType
using Nabd.Core.Enums.Medical;

namespace Nabd.Application.Mappers
{
    // هذا الملف مسؤول عن تحويل البيانات بين DTOs التسجيل والدخول وكيانات الهوية والأدوار.
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            // ==========================================
            // I. Registration Mappings (DTO -> Entity)
            // ==========================================

            // 1. RegisterDoctorDto => AppUser Entity
            CreateMap<RegisterDoctorDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // استخدام الإيميل كاسم مستخدم
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Doctor)) // تثبيت الدور
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore()) // يتم التأكيد لاحقاً
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true)); // تفعيل نظام الحظر

            // 2. RegisterPatientDto => AppUser Entity
            CreateMap<RegisterPatientDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Patient))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true));

            // 3. RegisterDoctorDto => Doctor Entity
            // ربط الخصائص المهنية مباشرةً
            CreateMap<RegisterDoctorDto, Doctor>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => DoctorStatus.Pending)) // دكتور جديد دائمًا حالته "في الانتظار"
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // 4. RegisterPatientDto => Patient Entity
            CreateMap<RegisterPatientDto, Patient>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.Ignore()); // Age خاصية محسوبة (NotMapped)

            // ==========================================
            // II. Authentication Response Mappings (Entity -> DTO)
            // ==========================================

            // 5. AppUser Entity => AuthResponseDto
            // هذا Mapping يُستخدم لتكوين الرد النهائي للدخول والتسجيل.
            CreateMap<AppUser, AuthResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.IsAuthSuccessful, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Token, opt => opt.Ignore()) // يتم تعيين الـ Token يدوياً في AuthService
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore()); // يتم تعيين الـ RefreshToken يدوياً في AuthService

            // 6. RefreshToken Entity => AuthResponseDto
            // Mapping خاص لتمرير التوكن فقط (في عملية RenewToken)
            CreateMap<RefreshToken, AuthResponseDto>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.IsAuthSuccessful, opt => opt.MapFrom(src => true));
        }
    }
}