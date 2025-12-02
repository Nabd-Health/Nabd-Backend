using AutoMapper;
using Nabd.Application.DTOs.Medical; // ConsultationRecordRequest, Response
using Nabd.Application.DTOs.AI;      // AIFeedbackRequest, AIDiagnosisResultDto
using Nabd.Core.Entities.Medical;    // ConsultationRecord, MedicalHistoryItem
using Nabd.Core.Entities.AI;         // AIDiagnosisLog
using System;

namespace Nabd.Application.Mappers
{
    public class MedicalMappingProfile : Profile
    {
        public MedicalMappingProfile()
        {
            // ==========================================
            // I. Consultation Record Mappings (AI Input)
            // ==========================================

            // 1. Request -> Entity
            CreateMap<CreateConsultationRecordRequest, ConsultationRecord>()
                .ForMember(dest => dest.WasAIAssisted, opt => opt.MapFrom(src => false)) // افتراضي
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // 2. Entity -> Response DTO (Full Details)
            CreateMap<ConsultationRecord, ConsultationRecordResponse>()
                // التأكد من أن Appointment و Doctor محملين (Included)
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Doctor.FullName : ""))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Patient.FullName : ""))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Doctor.Specialization.ToString() : ""));

            // 3. Entity -> Summary DTO (For Lists)
            CreateMap<ConsultationRecord, ConsultationRecordSummaryDto>()
                .ForMember(dest => dest.VisitDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Doctor.FullName : ""))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Doctor.Specialization.ToString() : ""))
                .ForMember(dest => dest.Diagnosis, opt => opt.MapFrom(src => src.FinalDiagnosis));

            // ==========================================
            // II. AI Log Mappings (Output & Feedback)
            // ==========================================

            // 4. Entity -> DTO (للعرض في الداشبورد)
            CreateMap<AIDiagnosisLog, AIDiagnosisResultDto>()
                // تصحيح: اسم الخاصية في DTO هو ProcessingDurationMs (مطابق للـ Entity)
                .ForMember(dest => dest.ProcessingDurationMs, opt => opt.MapFrom(src => src.ProcessingDurationMs))
                // تصحيح: اسم الخاصية في DTO هو AnalysisDate
                .ForMember(dest => dest.AnalysisDate, opt => opt.MapFrom(src => src.RequestTimestamp))
                // تصحيح: DTO لا يحتوي على Confidence في الجذر، لذلك نتجاهلها هنا 
                // (سنقوم بملء قائمة Predictions يدوياً في الـ Service لأنها تحتاج تحويل من JSON)
                .ForMember(dest => dest.Predictions, opt => opt.Ignore());

            // 5. Feedback Request -> Entity
            CreateMap<AIFeedbackRequest, AIDiagnosisLog>()
                .ForMember(dest => dest.DoctorAction, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.CorrectedDiagnosis, opt => opt.MapFrom(src => src.CorrectedDiagnosis))
                .ForMember(dest => dest.FeedbackNotes, opt => opt.MapFrom(src => src.Comments));

            // ==========================================
            // III. Medical History Mappings
            // ==========================================

            // 6. Request -> Entity
            CreateMap<CreateMedicalHistoryItemRequest, MedicalHistoryItem>();

            // 7. Entity -> Response DTO
            CreateMap<MedicalHistoryItem, MedicalHistoryItemResponse>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate)) // التاريخ الطبي
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.EventType.ToString())); // Enum to String
        }
    }
}