using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Operations;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Helper: لجلب الـ UserId الخاص بالـ Identity (للدكاترة فقط أو العمليات العامة)
        private Guid GetCurrentUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return id == null ? Guid.Empty : Guid.Parse(id);
        }

        // ==========================================
        // 1.  (Booking) 
        // ==========================================
        [HttpPost("book")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1. نجيب إيميل المستخدم الحالي
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            // 2. نجيب المريض الحقيقي من الداتابيز عشان ناخد الـ ID بتاعه
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email);
            if (patient == null) return BadRequest("ملف المريض غير موجود.");

            // 3. نتأكد من الدكتور
            var doctor = await _unitOfWork.Doctors.GetByIdWithSchedulesAsync(dto.DoctorId);
            if (doctor == null) return NotFound("Doctor not found.");

            // 4. التحقق من التعارض
            var endTime = dto.AppointmentDate.AddMinutes(doctor.SessionDurationMinutes);
            var hasConflict = await _unitOfWork.Appointments.HasConflictingAppointmentAsync(
                dto.DoctorId, dto.AppointmentDate, endTime);

            if (hasConflict)
            {
                return Conflict(new { Message = "هذا الموعد محجوز بالفعل، يرجى اختيار موعد آخر." });
            }

            // 5. إنشاء الحجز
            var appointment = _mapper.Map<Appointment>(dto);

           
            appointment.PatientId = patient.Id;

            appointment.Status = AppointmentStatus.Pending;
            appointment.Price = doctor.ConsultationFee;
            appointment.EstimatedDurationMinutes = doctor.SessionDurationMinutes;

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AppointmentResponse>(appointment);
            response.DoctorName = doctor.FullName;

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, response);
        }

        // ==========================================
        // 2.  (Retrieval)
        // ==========================================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdWithDetailsAsync(id);
            if (appointment == null) return NotFound("Appointment not found.");

            var currentUserId = GetCurrentUserId(); // AppUserId
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            //(Security Check)
            if (userRole == "Doctor")
            {
              
                var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
                if (doctor == null || appointment.DoctorId != doctor.Id) return Forbid();
            }

            if (userRole == "Patient")
            {
                var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);
                if (patient == null || appointment.PatientId != patient.Id) return Forbid();
            }

            var response = _mapper.Map<AppointmentResponse>(appointment);
            return Ok(response);
        }

        [HttpGet("my-appointments")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyAppointments()
        {
            
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);

            if (patient == null) return NotFound("Patient profile not found");

            var appointments = await _unitOfWork.Appointments.GetByPatientIdAsync(patient.Id);
            var result = _mapper.Map<IEnumerable<AppointmentResponse>>(appointments);
            return Ok(result);
        }

        [HttpGet("doctor/requests")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorAppointments(
            [FromQuery] DateTime? date,
            [FromQuery] AppointmentStatus? status)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
            if (doctor == null) return Unauthorized();

            var result = await _unitOfWork.Appointments.GetByDoctorIdWithFiltersAsync(
                doctor.Id,
                date?.Date,
                date?.Date.AddDays(1),
                status,
                1, 50, "Date", "Asc");

            var response = _mapper.Map<IEnumerable<AppointmentResponse>>(result.Appointments);
            return Ok(response);
        }

        // ==========================================
        // 3.  (Actions) - للدكتور
        // ==========================================

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] AppointmentStatus newStatus)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);

            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            if (appointment.DoctorId != doctor!.Id) return Forbid();

            appointment.Status = newStatus;

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = $"Appointment status updated to {newStatus}" });
        }

        // ==========================================
        // 4.  (Cancel) - للطرفين
        // ==========================================
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(Guid id, [FromBody] string reason)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            bool isAuthorized = false;
            bool cancelledByPatient = false;

            if (role == "Patient")
            {
                var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);
                if (patient != null && appointment.PatientId == patient.Id)
                {
                    isAuthorized = true;
                    cancelledByPatient = true;
                }
            }
            else if (role == "Doctor")
            {
                var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
                if (doctor != null && appointment.DoctorId == doctor.Id)
                {
                    isAuthorized = true;
                }
            }

            if (!isAuthorized) return Forbid();

            if (appointment.Status == AppointmentStatus.Completed)
                return BadRequest("Cannot cancel a completed appointment.");

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancellationReason = reason;
            appointment.CancelledByPatient = cancelledByPatient;

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Appointment cancelled successfully." });
        }
    }
}