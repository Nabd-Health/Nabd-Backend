using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.AI
{
    
    public class AIPrescriptionAnalysisResultDto
    {
        // ==========================================
        // 1. Overall Status 
        // ==========================================

     
        public bool IsSafe { get; set; }

        
        public string OverallRiskLevel { get; set; } = "Safe";

        // ==========================================
        // 2. MLOps Metadata 
        // ==========================================

        public required string ModelName { get; set; }
        public required string ModelVersion { get; set; }
        public double AnalysisTimeMs { get; set; } // زمن التحليل

        // ==========================================
        // 3. The Findings (النتائج والتحذيرات)
        // ==========================================

        
        public List<PrescriptionAlertDto> Alerts { get; set; } = new();

        
        public List<SmartRecommendationDto> Recommendations { get; set; } = new();
    }

    
    public class PrescriptionAlertDto
    {
       
        public required string AlertType { get; set; }

        public required string Severity { get; set; }

        public required string Title { get; set; }

        
        public string? Description { get; set; }

      
        public List<string> ConflictingElements { get; set; } = new();

        
        public string? ReferenceSource { get; set; }
    }

  
    public class SmartRecommendationDto
    {
        public required string Type { get; set; }

        public required string Suggestion { get; set; }

        public string? Reasoning { get; set; }

        public string? SuggestedActionCode { get; set; }
    }
}