using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Pharmacy;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class PrescriptionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrescriptionsController(IUnitOfWork unitOfWork, IMapper mapper)
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
        // 1. إنشاء روشتة (للدكتور)
        // ==========================================
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doctorId = GetCurrentUserId();

   
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
            if (appointment == null) return NotFound("الموعد غير موجود.");

            if (appointment.DoctorId != doctorId)
                return Forbid("لا يمكنك كتابة روشتة لموعد ليس لك.");

          
            var consultation = await _unitOfWork.ConsultationRecords.GetByAppointmentIdAsync(dto.AppointmentId);
            if (consultation == null)
            {
               
                return BadRequest("يجب بدء الكشف (Consultation) قبل كتابة الروشتة.");
            }

        
            var prescription = _mapper.Map<Prescription>(dto);
            prescription.DoctorId = doctorId;
            prescription.PatientId = appointment.PatientId;
            prescription.ConsultationRecordId = consultation.Id;

         

            await _unitOfWork.Prescriptions.AddAsync(prescription);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<PrescriptionResponse>(prescription);
            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, response);
        }

        // ==========================================
        // 2. عرض روشتة (للدكتور والمريض)
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(Guid id)
        {
            var prescription = await _unitOfWork.Prescriptions.GetByIdWithDetailsAsync(id);
            if (prescription == null) return NotFound("الروشتة غير موجودة.");

            var userId = GetCurrentUserId();
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            
            if (userRole == "Doctor" && prescription.DoctorId != userId) return Forbid();
            if (userRole == "Patient" && prescription.PatientId != userId) return Forbid();

            var response = _mapper.Map<PrescriptionResponse>(prescription);
            return Ok(response);
        }

        // ==========================================
        // 3. عرض كل روشتات المريض (التاريخ الدوائي)
        // ==========================================
        [HttpGet("my-history")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyPrescriptions()
        {
            var patientId = GetCurrentUserId();
            var prescriptions = await _unitOfWork.Prescriptions.GetByPatientIdAsync(patientId);

            var response = _mapper.Map<IEnumerable<PrescriptionResponse>>(prescriptions);
            return Ok(response);
        }

        // ==========================================
        // 4. البحث عن دواء (Auto-Complete)
        // ==========================================
        
        [HttpGet("medications/search")]
        public async Task<IActionResult> SearchMedications([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return Ok(new List<object>());

            var meds = await _unitOfWork.Medications.SearchAsync(term);

            var result = meds.Select(m => new { m.Id, Name = $"{m.TradeName} ({m.Strength})" });

            return Ok(result);
        }
    }
}