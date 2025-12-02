using AutoMapper;
using Nabd.Application.DTOs.Medical;
using Nabd.Application.DTOs.Profiles;
using Nabd.Core.DTOs;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Profiles;
using System;

namespace Nabd.Application.Mappers
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            // ==========================================
            // I. Patient Profile Views
            // ==========================================

            CreateMap<Patient, PatientProfileResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.AppUser != null ? $"{src.AppUser.FirstName} {src.AppUser.LastName}" : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>
                    src.AppUser != null ? src.AppUser.Email : string.Empty))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src =>
                    src.AppUser != null ? src.AppUser.ProfilePictureUrl : null))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

            CreateMap<Patient, PatientFullProfileDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.AppUser != null ? $"{src.AppUser.FirstName} {src.AppUser.LastName}" : string.Empty))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src =>
                    src.AppUser != null ? src.AppUser.ProfilePictureUrl : null));

            CreateMap<Patient, DoctorPatientDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.FirstName : string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.LastName : string.Empty))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.ProfilePictureUrl : null));

            // ==========================================
            // II. Medical History Mappings
            // ==========================================

            // 4. Request -> Entity
            CreateMap<CreateMedicalHistoryItemRequest, MedicalHistoryItem>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.Type));

            // 5. Entity -> Response
            CreateMap<MedicalHistoryItem, MedicalHistoryItemResponse>()
                // ✅ [التصحيح]: يجب استخدام EventType (الخاصية في الـ Entity)
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.EventType.ToString()))
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate));
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}