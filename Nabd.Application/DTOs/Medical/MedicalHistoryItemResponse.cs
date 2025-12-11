using System;

namespace Nabd.Application.DTOs.Medical
{
    public class MedicalHistoryItemResponse
    {
        public Guid Id { get; set; }


        public string Type { get; set; } = string.Empty;


        public string Title { get; set; } = string.Empty;


        public string? Details { get; set; }

  
        public DateTime EventDate { get; set; }

        public bool IsCritical { get; set; }
    }
}