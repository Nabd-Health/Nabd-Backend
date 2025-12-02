using AutoMapper;
using Nabd.Application.DTOs.Operations;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using System;
using System.Linq;

namespace Nabd.Application.Mappers
{
    public class OperationsMappingProfile : Profile
    {
        public OperationsMappingProfile()
        {
            // ==========================================
            // I. Appointment Mappings (الحجز والجدولة)
            // ==========================================

            CreateMap<Appointment, AppointmentResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.PatientAge, opt => opt.MapFrom(src =>
                    src.Patient != null ? CalculateAge(src.Patient.DateOfBirth) : (int?)null))
                .ForMember(dest => dest.PatientPhoneNumber, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.PhoneNumber : null))

                // ✅ [التصحيح الحاسم]: يجب التحقق من AppUser قبل الوصول للصورة
                .ForMember(dest => dest.PatientProfileImageUrl, opt => opt.MapFrom(src =>
                    src.Patient != null && src.Patient.AppUser != null ? src.Patient.AppUser.ProfilePictureUrl : null))

                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                    src.Doctor != null ? src.Doctor.FullName : string.Empty))
                .ForMember(dest => dest.ClinicBranchName, opt => opt.Ignore())
                .ForMember(dest => dest.HasPrescription, opt => opt.MapFrom(src =>
                    src.ConsultationRecord != null && src.ConsultationRecord.Prescriptions.Any()));

            CreateMap<BookAppointmentRequest, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Nabd.Core.Enums.Operations.AppointmentStatus.Pending));

            // ==========================================
            // II. Clinic Branch Mappings (إدارة العيادة)
            // ==========================================

            CreateMap<ClinicBranch, ClinicBranchResponse>();

            CreateMap<CreateClinicBranchRequest, ClinicBranch>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
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