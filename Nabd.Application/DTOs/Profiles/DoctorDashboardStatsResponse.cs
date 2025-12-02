namespace Nabd.Application.DTOs.Profiles
{
    public class DoctorDashboardStatsResponse
    {
        public int TotalPatients { get; set; }
        public int TodayAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int PendingAppointments { get; set; }
        public int CancelledAppointments { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}