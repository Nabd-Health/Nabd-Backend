using AutoMapper;
using Nabd.Application.DTOs.Doctors;
using Nabd.Application.DTOs.Operations; 
using Nabd.Application.DTOs.Profiles;   
using Nabd.Core.Entities.Operations;    
using Nabd.Core.Entities.Profiles;     
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;         

namespace Nabd.Application.Mappers
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            // ==========================================
            // 1. Doctor Profile Mappings 
            // ==========================================

            // Entity -> Response DTO
            CreateMap<Doctor, DoctorProfileResponse>()
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Status == DoctorStatus.Active)) 
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.ClinicBranches)); 

            // Request DTO -> Entity (Patch Update)
            CreateMap<UpdateDoctorProfileRequest, Doctor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==========================================
            // 2. Doctor Schedule Mappings 
            // ==========================================

            // Entity -> Response DTO
            CreateMap<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.ClinicBranchName, opt => opt.MapFrom(src => src.ClinicBranch.Name)) 
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.DayOfWeek.ToString())); // تحويل الـ Enum لنص

         
            CreateMap<CreateDoctorScheduleRequest, DoctorSchedule>();
        
            CreateMap<DoctorDocument, DoctorDocumentResponseDto>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()));
          
        }
    }
}