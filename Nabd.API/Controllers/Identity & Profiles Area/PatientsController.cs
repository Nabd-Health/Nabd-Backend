using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Patients; 
using Nabd.Application.DTOs.Profiles; 
using Nabd.Core.Entities.Medical;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")] 
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientsController(IUnitOfWork unitOfWork, IMapper mapper)
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
        // 1.  (My Profile)
        // ==========================================
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email!);

            if (patient == null) return NotFound("Patient profile not found.");

           
            var fullProfile = await _unitOfWork.Patients.GetByIdWithDetailsAsync(patient.Id);

            var result = _mapper.Map<PatientFullProfileDto>(fullProfile);
            return Ok(result);
        }

        // ==========================================
        // 2. (Update Info)
        // ==========================================
        [HttpPut("update-info")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePatientProfileRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email);

            if (patient == null) return NotFound();

            if (dto.PhoneNumber != null) patient.PhoneNumber = dto.PhoneNumber;
            if (dto.Address != null) patient.Address = dto.Address;
            if (dto.City != null) patient.City = dto.City;
            if (dto.JobTitle != null) patient.JobTitle = dto.JobTitle;
            if (dto.MaritalStatus != null) patient.MaritalStatus = dto.MaritalStatus;

            if (dto.EmergencyContactName != null) patient.EmergencyContactName = dto.EmergencyContactName;
            if (dto.EmergencyContactPhone != null) patient.EmergencyContactPhone = dto.EmergencyContactPhone;

        
            patient.HasInsurance = dto.HasInsurance;
            if (dto.InsuranceProvider != null) patient.InsuranceProvider = dto.InsuranceProvider;
            if (dto.InsuranceNumber != null) patient.InsuranceNumber = dto.InsuranceNumber;

            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Profile updated successfully" });
        }

        // ==========================================
        // 3. (Medical History)
        // ==========================================

        [HttpGet("medical-history")]
        public async Task<IActionResult> GetMedicalHistory()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email);

            var history = await _unitOfWork.MedicalHistoryItems.GetByPatientIdAsync(patient!.Id);
            var result = _mapper.Map<IEnumerable<MedicalHistoryItemResponse>>(history);

            return Ok(result);
        }

        [HttpPost("medical-history")]
        public async Task<IActionResult> AddMedicalHistoryItem([FromBody] CreateMedicalHistoryItemRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email);

            var item = _mapper.Map<MedicalHistoryItem>(dto);
            item.PatientId = patient!.Id;

            await _unitOfWork.MedicalHistoryItems.AddAsync(item);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Medical history item added", ItemId = item.Id });
        }

        [HttpDelete("medical-history/{id}")]
        public async Task<IActionResult> DeleteMedicalHistoryItem(Guid id)
        {
            var item = await _unitOfWork.MedicalHistoryItems.GetByIdAsync(id);
            if (item == null) return NotFound();

            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var patient = await _unitOfWork.Patients.GetByEmailAsync(email);

            if (item.PatientId != patient!.Id)
                return Forbid("You can only delete your own history.");

            _unitOfWork.MedicalHistoryItems.Delete(item);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Item deleted successfully" });
        }
    }
}