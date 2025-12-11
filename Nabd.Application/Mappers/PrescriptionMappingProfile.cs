using AutoMapper;
using Nabd.Application.DTOs.Pharmacy;
using Nabd.Core.Entities.Pharmacy;    
using Nabd.Core.Enums;               
using System;

namespace Nabd.Application.Mappers
{
    public class PrescriptionMappingProfile : Profile
    {
        public PrescriptionMappingProfile()
        {
            // ==========================================
            // I. Prescription Mappings 
            // ==========================================

            // 1. Entity -> Response DTO
            CreateMap<Prescription, PrescriptionResponse>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                    src.Doctor != null ? src.Doctor.FullName : string.Empty))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                    src.Patient != null ? src.Patient.FullName : string.Empty))
              
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PrescriptionItems));

            // 2. Request DTO -> Entity
            CreateMap<CreatePrescriptionRequest, Prescription>()
             

                .ForMember(dest => dest.PrescriptionItems, opt => opt.MapFrom(src => src.Items))

                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            // ==========================================
            // II. Prescription Item Mappings 
            // ==========================================

            // 3. Entity -> Response DTO 
            CreateMap<PrescriptionItem, PrescriptionItemDto>()
                .ForMember(dest => dest.MedicationName, opt => opt.MapFrom(src => src.Medication.TradeName))
                .ForMember(dest => dest.ScientificName, opt => opt.MapFrom(src => src.Medication.ScientificName))
                .ForMember(dest => dest.Strength, opt => opt.MapFrom(src => src.Medication.Strength))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Medication.Form.ToString()));

            // 4. Request DTO -> Entity 
            CreateMap<CreatePrescriptionItemDto, PrescriptionItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MedicationId, opt => opt.MapFrom(src => src.MedicationId))
   
                ;

            // ==========================================
            // III. Medication Mappings 
            // ==========================================

            // 5. Entity -> DTO
            CreateMap<Medication, MedicationDto>()
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => src.Form.ToString()));
        }
    }
}