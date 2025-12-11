using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Medical; 
using Nabd.Core.Entities.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       

        public ConsultationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private Guid GetCurrentUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return id == null ? Guid.Empty : Guid.Parse(id);
        }

        // ==========================================
        // 1.  (Start Consultation)
        // ==========================================
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreateConsultation([FromBody] CreateConsultationRecordRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doctorId = GetCurrentUserId();

            
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
            if (appointment == null) return NotFound("Appointment not found.");

            if (appointment.DoctorId != doctorId)
                return Forbid("You can only create records for your own appointments.");

            
            var existingRecord = await _unitOfWork.ConsultationRecords.GetByAppointmentIdAsync(dto.AppointmentId);
            if (existingRecord != null)
                return Conflict(new { Message = "Consultation record already exists for this appointment." });

         
            var record = _mapper.Map<ConsultationRecord>(dto);


            appointment.Status = AppointmentStatus.InProgress;
            _unitOfWork.Appointments.Update(appointment);

            await _unitOfWork.ConsultationRecords.AddAsync(record);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<ConsultationRecordResponse>(record);
            return CreatedAtAction(nameof(GetConsultationById), new { id = record.Id }, response);
        }

        // ==========================================
        // 2.  (Get Details)
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationById(Guid id)
        {
            var record = await _unitOfWork.ConsultationRecords.GetByIdWithDetailsAsync(id); 
            if (record == null) return NotFound("Consultation record not found.");

            var userId = GetCurrentUserId();
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

      
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(record.AppointmentId);

            if (role == "Doctor" && appointment?.DoctorId != userId) return Forbid();
            if (role == "Patient" && appointment?.PatientId != userId) return Forbid();

            var response = _mapper.Map<ConsultationRecordResponse>(record);
            return Ok(response);
        }

        // ==========================================
        // 3. (Update Diagnosis)
        // ==========================================
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateConsultation(Guid id, [FromBody] UpdateConsultationRecordRequest dto)
        {
            var record = await _unitOfWork.ConsultationRecords.GetByIdAsync(id);
            if (record == null) return NotFound();

            var appointment = await _unitOfWork.Appointments.GetByIdAsync(record.AppointmentId);
            if (appointment?.DoctorId != GetCurrentUserId()) return Forbid();


            record.Symptoms = dto.Symptoms ?? record.Symptoms;
            record.FinalDiagnosis = dto.FinalDiagnosis ?? record.FinalDiagnosis;
            record.TreatmentPlan = dto.TreatmentPlan ?? record.TreatmentPlan;


            if (dto.Weight.HasValue) record.WeightAtVisit = dto.Weight;


            _unitOfWork.ConsultationRecords.Update(record);


            if (dto.MarkAsCompleted)
            {
                appointment!.Status = AppointmentStatus.Completed;
                _unitOfWork.Appointments.Update(appointment);
            }

            await _unitOfWork.CompleteAsync();
            return Ok(new { Message = "Consultation updated successfully." });
        }

        // ==========================================
        // 4.(AI Request)
        // ==========================================
        [HttpPost("{id}/ai-suggest")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> RequestAISuggestion(Guid id)
        {
            var record = await _unitOfWork.ConsultationRecords.GetByIdAsync(id);
            if (record == null) return NotFound();

 

            // (Mock)
            var mockResponse = new
            {
                Prediction = "Seasonal Flu",
                Confidence = 0.92,
                RecommendedAction = "Rest and Fluids"
            };

            return Ok(mockResponse);
        }
    }
}