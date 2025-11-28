using AutoMapper;
using Nabd.Core.Entities;
using Nabd.Core.Enums;
using Nabd.Application.DTOs.Auth;
using Nabd.Application.DTOs.Doctor;
using Nabd.Application.DTOs.Patient;
using System;
using System.Linq;

namespace Nabd.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // أ) تسجيل الدكتور (RegisterDoctorDto -> AppUser)
            CreateMap<RegisterDoctorDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Doctor))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            // ب) تسجيل الدكتور (RegisterDoctorDto -> Doctor Profile)
            CreateMap<RegisterDoctorDto, Doctor>();

            // ... (باقي كود الـ Mapper) ...

            // ج) رد المصادقة (AppUser -> AuthResponseDto)
            CreateMap<AppUser, AuthResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserType.ToString()));
        }
    }
}