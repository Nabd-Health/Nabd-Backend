using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Feedback
{
    /// <summary>
    /// الرد الشامل للتقييم (يستخدم للقائمة وللتفاصيل)
    /// </summary>
    public class DoctorReviewResponse
    {
        public Guid Id { get; set; }

        // Context
        public Guid AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Patient Info (مع مراعاة الخصوصية)
        public string PatientName { get; set; } = string.Empty; // "Anonymous" if IsAnonymous=true
        public string? PatientProfileImageUrl { get; set; } // null if IsAnonymous=true

        // The 5-Star Metrics
        public double AverageRating { get; set; } // (Calculated)
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

    /// <summary>
    /// إحصائيات التقييمات (لعرضها في أعلى صفحة الطبيب)
    /// </summary>
    public class DoctorReviewStatsDto
    {
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }

        // توزيع النجوم (كم واحد أعطى 5 نجوم، 4 نجوم...)
        // Key: "5 Stars", Value: 150
        public Dictionary<string, int> RatingDistribution { get; set; } = new();
    }
}