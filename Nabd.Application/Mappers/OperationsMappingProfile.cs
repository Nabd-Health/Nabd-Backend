using AutoMapper;
using Nabd.Application.DTOs.Operations; 
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Enums.Operations;
using System;
using System.Linq;

namespace Nabd.Application.Mappers
{
    public class OperationsMappingProfile : Profile
    {
        public OperationsMappingProfile()
        {
            // ==========================================
            // I. Appointment Mappings 
            // ==========================================

            CreateMap<Appointment, AppointmentResponse>()
                // 1. بيانات المريض
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.FullName : "Unknown"))

                .ForMember(dest => dest.PatientPhoneNumber, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.PhoneNumber : null))

                .ForMember(dest => dest.PatientAge, opt => opt.MapFrom(src =>
                    src.Patient != null ? CalculateAge(src.Patient.DateOfBirth) : (int?)null))

             
                .ForMember(dest => dest.PatientProfileImageUrl, opt => opt.MapFrom(src =>
                    src.Patient != null && src.Patient.AppUser != null ? src.Patient.AppUser.ProfilePictureUrl : null))

                .ForMember(dest => dest.PatientGender, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.Gender.ToString() : string.Empty))

                // 2. بيانات الطبيب
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                    src.Doctor != null ? src.Doctor.FullName : string.Empty))

                // 3. تفاصيل الموعد
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())) 
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))    

                .ForMember(dest => dest.HasPrescription, opt => opt.MapFrom(src =>
                    src.ConsultationRecord != null && src.ConsultationRecord.Prescriptions.Any()));

       
            CreateMap<BookAppointmentRequest, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.Pending))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // ==========================================
            // II. Clinic Branch Mappings 
            // ==========================================

            CreateMap<ClinicBranch, ClinicBranchResponse>()

                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.City}, {src.StreetAddress}"))
                .ForMember(dest => dest.Governorate, opt => opt.MapFrom(src => src.Governorate.ToString()));

            CreateMap<CreateClinicBranchRequest, ClinicBranch>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Entity -> Response
            CreateMap<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.ClinicBranchName, opt => opt.MapFrom(src => src.ClinicBranch.Name))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.DayOfWeek.ToString())) 
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => $"{src.StartTime:hh\\:mm} - {src.EndTime:hh\\:mm}"))

               
                .ForMember(dest => dest.BreakTime, opt => opt.MapFrom(src =>
                    src.HasBreak && src.BreakStartTime.HasValue && src.BreakEndTime.HasValue
                    ? $"{src.BreakStartTime.Value:hh\\:mm} - {src.BreakEndTime.Value:hh\\:mm}"
                    : null));

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