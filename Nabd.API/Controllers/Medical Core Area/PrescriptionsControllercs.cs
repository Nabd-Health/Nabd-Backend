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

        // ==========================================
        // 1. إنشاء روشتة (للدكتور)
        // ==========================================
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1.
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);

            if (doctor == null) return Unauthorized();

            // 2. نجيب الموعد
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
            if (appointment == null) return NotFound("الموعد غير موجود.");

            // 3. التحقق: هل الموعد يخص هذا الدكتور؟
            if (appointment.DoctorId != doctor.Id)
            {
             
                return StatusCode(403, new { Message = "لا يمكنك كتابة روشتة لموعد ليس لك." });
            }

            // 4. التأكد من وجود كشف (Consultation) لهذا الموعد
            var consultation = await _unitOfWork.ConsultationRecords.GetByAppointmentIdAsync(dto.AppointmentId);
            if (consultation == null)
            {
                return BadRequest("يجب بدء الكشف (Consultation) قبل كتابة الروشتة.");
            }

            // 5. إنشاء الروشتة
            var prescription = _mapper.Map<Prescription>(dto);

            // ربط الروشتة بالدكتور والمريض والكشف
            prescription.DoctorId = doctor.Id;
            prescription.PatientId = appointment.PatientId;
            prescription.ConsultationRecordId = consultation.Id;

            await _unitOfWork.Prescriptions.AddAsync(prescription);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<PrescriptionResponse>(prescription);
            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, response);
        }

        // ==========================================
        // 2. عرض روشتة (للدكتور والمريض
        // ==========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(Guid id)
        {
            var prescription = await _unitOfWork.Prescriptions.GetByIdWithDetailsAsync(id);
            if (prescription == null) return NotFound("الروشتة غير موجودة.");

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Doctor")
            {
                var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
                if (doctor == null || prescription.DoctorId != doctor.Id) return StatusCode(403);
            }
            else if (role == "Patient")
            {
                var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);
                if (patient == null || prescription.PatientId != patient.Id) return StatusCode(403);
            }

            var response = _mapper.Map<PrescriptionResponse>(prescription);
            return Ok(response);
        }

        // ==========================================
        // 3. عرض كل روشتات المريض (التاريخ الدوائي
        // ==========================================
        [HttpGet("my-history")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetMyPrescriptions()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);

            if (patient == null) return NotFound();

            var prescriptions = await _unitOfWork.Prescriptions.GetByPatientIdAsync(patient.Id);
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