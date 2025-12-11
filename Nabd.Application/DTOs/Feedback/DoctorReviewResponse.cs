using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Feedback
{
  
    public class DoctorReviewResponse
    {
        public Guid Id { get; set; }

        // Context
        public Guid AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Patient Info 
        public string PatientName { get; set; } = string.Empty; 
        public string? PatientProfileImageUrl { get; set; } 

        // The 5-Star Metrics
        public double AverageRating { get; set; } 
        public int OverallSatisfaction { get; set; }
        public int WaitingTime { get; set; }
        public int CommunicationQuality { get; set; }
        public int ClinicCleanliness { get; set; }
        public int ValueForMoney { get; set; }

        // Content
        public string? Comment { get; set; }

        // Doctor Interaction
        public string? DoctorReply { get; set; }
        public DateTime? DoctorRepliedAt { get; set; }
    }


    public class DoctorReviewStatsDto
    {
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }


        public Dictionary<string, int> RatingDistribution { get; set; } = new();
    }
}