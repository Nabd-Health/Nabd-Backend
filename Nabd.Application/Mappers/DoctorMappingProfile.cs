using AutoMapper;
using Nabd.Application.DTOs.Operations; // لـ DoctorScheduleResponse
using Nabd.Application.DTOs.Profiles;   // لـ DoctorProfileResponse, UpdateDoctorProfileRequest
using Nabd.Core.Entities.Operations;    // لـ DoctorSchedule Entity
using Nabd.Core.Entities.Profiles;      // لـ Doctor Entity
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;         // لـ DoctorStatus

namespace Nabd.Application.Mappers
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            // ==========================================
            // 1. Doctor Profile Mappings (الملف الشخصي)
            // ==========================================

            // Entity -> Response DTO
            CreateMap<Doctor, DoctorProfileResponse>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Status == DoctorStatus.Active)) // افتراض أن Active تعني موثق
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.ClinicBranches)); // ربط الفروع

            // Request DTO -> Entity (Patch Update)
            CreateMap<UpdateDoctorProfileRequest, Doctor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==========================================
            // 2. Doctor Schedule Mappings (الجدولة)
            // ==========================================

            // Entity -> Response DTO
            CreateMap<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.ClinicBranchName, opt => opt.MapFrom(src => src.ClinicBranch.Name)) // جلب اسم العيادة
                .ForMember(dest => dest.DayName, opt => opt.MapFrom(src => src.DayOfWeek.ToString())); // تحويل الـ Enum لنص

            // Request DTO -> Entity
            CreateMap<CreateDoctorScheduleRequest, DoctorSchedule>();

            // ==========================================
            // 3. Cleaned Up Sections (تم الحذف)
            // ==========================================
            // - تم حذف DoctorConsultation (لأن الأسعار أصبحت في DoctorProfile)
            // - تم حذف DoctorDocument (مؤقتاً، سنتعامل معها كـ File Upload مباشر)
            // - تم حذف DoctorBasicResponse (استبدلناه بـ DoctorProfileResponse)
        }
    }
}