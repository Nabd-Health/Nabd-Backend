using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Doctors;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorDashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ==========================================
        // 1. جلب إحصائيات الصفحة الرئيسية
        // ==========================================
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);
            if (doctor == null) return BadRequest("Doctor profile not found.");


            var todayCount = await _unitOfWork.Appointments.GetTodayAppointmentsCountAsync(doctor.Id);
            var patientsCount = await _unitOfWork.Appointments.GetUniquePatientsCountAsync(doctor.Id);
            var pendingCount = await _unitOfWork.Appointments.GetPendingAppointmentsCountAsync(doctor.Id);
            var revenue = await _unitOfWork.Appointments.GetTotalRevenueAsync(doctor.Id);

           
            var upcoming = await _unitOfWork.Appointments.GetByDoctorIdWithFiltersAsync(
                doctorId: doctor.Id,
                startDate: DateTime.UtcNow,
                endDate: null,
                status: Nabd.Core.Enums.Operations.AppointmentStatus.Confirmed,
                pageNumber: 1,
                pageSize: 5,
                sortBy: "Date",
                sortOrder: "Asc");


            var stats = new DoctorDashboardStatsDto
            {
                TodayAppointmentsCount = todayCount,
                TotalPatientsCount = patientsCount,
                PendingRequestsCount = pendingCount,
                TotalRevenue = revenue,

                UpcomingAppointments = upcoming.Appointments.Select(a => new DashboardAppointmentDto
                {
                    AppointmentId = a.Id,
                    PatientName = a.Patient != null ? a.Patient.FullName : "Unknown", 
                    Time = a.AppointmentDate.ToLocalTime().ToString("hh:mm tt"),
                    Status = a.Status.ToString(),
                    Type = a.Type.ToString()
                }).ToList()
            };

            return Ok(stats);
        }

        // ==========================================
        // 2.  (Chart Data)
        // ==========================================
        [HttpGet("revenue-chart")]
        public async Task<IActionResult> GetRevenueChart([FromQuery] int year)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);

            if (doctor == null) return BadRequest("Doctor not found");

            var monthlyRevenue = new List<decimal>();

            for (int i = 1; i <= 12; i++)
            {
                var monthTotal = await _unitOfWork.Appointments.GetMonthlyRevenueAsync(doctor.Id, year, i);
                monthlyRevenue.Add(monthTotal);
            }

            return Ok(new { Year = year, Data = monthlyRevenue });
        }
    }
}