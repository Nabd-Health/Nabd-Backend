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
    public class DoctorProfileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

     
        private Guid GetCurrentUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return id == null ? Guid.Empty : Guid.Parse(id);
        }

        // ==========================================
        // 1. عرض البروفايل الشخصي
        // ==========================================
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);
            if (doctor == null) return NotFound("Doctor profile not found.");

           
            var result = new
            {
                doctor.FullName,
                doctor.Bio,
                doctor.PhoneNumber,
                doctor.ConsultationFee,
                doctor.SessionDurationMinutes,
                doctor.City,
                doctor.Address,
                doctor.ProfilePictureUrl,
                doctor.IsAvailable
            };

            return Ok(result);
        }

        // ==========================================
        // 2.  (Update Info)
        // ==========================================
        [HttpPut("update-info")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateDoctorProfileDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);

            if (doctor == null) return NotFound();

        
            if (dto.Bio != null) doctor.Bio = dto.Bio;
            if (dto.PhoneNumber != null) doctor.PhoneNumber = dto.PhoneNumber;
            if (dto.ConsultationFee.HasValue) doctor.ConsultationFee = dto.ConsultationFee.Value;
            if (dto.SessionDurationMinutes.HasValue) doctor.SessionDurationMinutes = dto.SessionDurationMinutes.Value;
            if (dto.City != null) doctor.City = dto.City;
            if (dto.ClinicAddress != null) doctor.Address = dto.ClinicAddress;

            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Profile updated successfully" });
        }

        // ==========================================
        // 3.  (On/Off)
        // ==========================================
        [HttpPatch("availability")]
        public async Task<IActionResult> ToggleAvailability([FromBody] UpdateAvailabilityDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);

            if (doctor == null) return NotFound();

            doctor.IsAvailable = dto.IsAvailable;

            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = $"Availability set to {dto.IsAvailable}" });
        }

        // ==========================================
        // 4.  (Upload Image)
        // ==========================================
        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfileImageDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);

            if (doctor == null) return NotFound();

   
        
            var fakeUrl = $"https://nabd-storage.com/doctors/{doctor.Id}.jpg";

            doctor.ProfilePictureUrl = fakeUrl;
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Photo uploaded", Url = fakeUrl });
        }
    }
}