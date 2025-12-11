using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Medical
{
    public class UpdateConsultationRecordRequest
    {
      
        public string? Symptoms { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public string? PrescriptionNotes { get; set; }

        public double? Weight { get; set; }
        public double? Temperature { get; set; }
        public int? SystolicBloodPressure { get; set; }
        public int? DiastolicBloodPressure { get; set; }

     
        public bool MarkAsCompleted { get; set; } = false;
    }
}