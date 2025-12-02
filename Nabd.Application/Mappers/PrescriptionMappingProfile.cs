using AutoMapper;
using Nabd.Application.DTOs.Pharmacy; // DTOs
using Nabd.Core.Entities.Pharmacy;    // Entities
using Nabd.Core.Enums;                // PrescriptionStatus
using System;

namespace Nabd.Application.Mappers
{
    public class PrescriptionMappingProfile : Profile
    {
        public PrescriptionMappingProfile()
        {
            // ==========================================
            // I. Prescription Mappings (الروشتة)
            // ==========================================

            // 1. Entity -> Response DTO
            CreateMap<Prescription, PrescriptionResponse>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                    src.Doctor != null ? src.Doctor.FullName : string.Empty))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)); // ربط قائمة الأدوية

            // 2. Request DTO -> Entity
            CreateMap<CreatePrescriptionRequest, Prescription>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PrescriptionStatus.Active)) // الحالة الافتراضية
                .ForMember(dest => dest.UniqueCode, opt => opt.Ignore()) // يتم توليده في الـ Service
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // (تم تعليق UpdatePrescriptionRequest مؤقتاً لعدم وجود DTO)
            // CreateMap<UpdatePrescriptionRequest, Prescription>()
            //    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==========================================
            // II. Prescription Item Mappings (عناصر الروشتة)
            // ==========================================

            // 3. Entity -> Response DTO (PrescriptionItemDto)
            CreateMap<PrescriptionItem, PrescriptionItemDto>()
                .ForMember(dest => dest.MedicationName, opt => opt.MapFrom(src => src.Medication.TradeName))
                .ForMember(dest => dest.ScientificName, opt => opt.MapFrom(src => src.Medication.ScientificName))
                .ForMember(dest => dest.Strength, opt => opt.MapFrom(src => src.Medication.Strength))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Medication.Form.ToString()));

            // 4. Request DTO -> Entity (CreatePrescriptionItemDto)
            CreateMap<CreatePrescriptionItemDto, PrescriptionItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // ==========================================
            // III. Medication Mappings (البحث عن الأدوية)
            // ==========================================

            // 5. Entity -> DTO (MedicationDto)
            CreateMap<Medication, MedicationDto>()
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Form.ToString()));

            // (تم حذف CreateMedicationRequest لأننا لن ندير مخزون الأدوية في نبض، سنكتفي بالبحث)
        }
    }
}