using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.AI
{
    // ==========================================
    // 1.  (Request)
    // ==========================================
    public class AIDiagnosisRequest
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "يجب إدخال وصف الأعراض")]
        [MinLength(3, ErrorMessage = "الوصف قصير جداً")]
        public string Symptoms { get; set; } = string.Empty;

        public int? Age { get; set; }
        public string? Gender { get; set; }

        // Vitals
        public double? Temperature { get; set; }
        public string? BloodPressure { get; set; }
    }

    // ==========================================
    // 2.(Response)
    // ==========================================
    public class AIDiagnosisResponse
    {
        public string RequestId { get; set; } = string.Empty;
        public DateTime AnalyzedAt { get; set; }
        public List<PredictedDiseaseDto> Predictions { get; set; } = new();
    }

  
    public class AIDiagnosisResultDto : AIDiagnosisResponse
    {
        public double ProcessingDurationMs { get; set; }
        public DateTime AnalysisDate { get; set; }
    }

    public class PredictedDiseaseDto
    {
        public string DiseaseName { get; set; } = string.Empty;
        public double ConfidenceScore { get; set; }


        public string Recommendation { get; set; } = string.Empty;
        public bool IsCritical { get; set; }
    }

    // ==========================================
    // 3.  (Feedback)
    // ==========================================
    public class AIFeedbackRequest
    {
        [Required]
        public string RequestId { get; set; } = string.Empty;

        public bool WasCorrect { get; set; }
        public string? CorrectedDiagnosis { get; set; } 
        public string? DoctorNotes { get; set; }

        public string Action
        {
            get => WasCorrect ? "Accepted" : "Rejected";
            set => WasCorrect = (value == "Accepted");
        }

        public string? Comments
        {
            get => DoctorNotes;
            set => DoctorNotes = value;
        }
    }
}