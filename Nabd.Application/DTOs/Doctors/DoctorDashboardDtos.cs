using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Doctors
{
    
    public class DoctorDashboardStatsDto
    {
        public int TodayAppointmentsCount { get; set; }
        public int TotalPatientsCount { get; set; }
        public int PendingRequestsCount { get; set; }
        public decimal TotalRevenue { get; set; } 

        
        public List<DashboardAppointmentDto> UpcomingAppointments { get; set; } = new();
    }


    public class DashboardAppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
        public string? Type { get; set; } 
    }
}