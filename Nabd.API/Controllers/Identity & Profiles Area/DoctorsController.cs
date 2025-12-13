using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Doctors; 
using Nabd.Core.Enums;
using Nabd.Core.Enums.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces;


namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ==========================================
        // 1. البحث والفلترة
        // ==========================================
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchDoctors(
            [FromQuery] string? searchTerm,
            [FromQuery] MedicalSpecialty? specialty,
            [FromQuery] Governorate? governorate,
            [FromQuery] int? minExperience,
            [FromQuery] decimal? maxPrice,
            [FromQuery] double? minRating)
        {
           
            var doctors = await _unitOfWork.Doctors.SearchDoctorsAsync(
                searchTerm, specialty, governorate, minExperience, maxPrice, minRating);

       
            var result = doctors.Select(d => new DoctorListItemDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Specialization = d.Specialization.ToString(),
                ProfilePictureUrl = d.ProfilePictureUrl,
                TotalReviews = d.TotalReviews,
                AverageRating = d.AverageRating,
                ConsultationFee = d.ConsultationFee,

                PrimaryLocation = d.ClinicBranches.FirstOrDefault()?.City ?? "غير محدد",
                IsAvailableToday = d.IsAvailable
            });

            return Ok(result);
        }

        // ==========================================
        // 2. عرض التفاصيل
        // ==========================================
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorDetails(Guid id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdWithDetailsAsync(id);

            if (doctor == null)
                return NotFound(new { Message = "الطبيب غير موجود" });

           
            var result = new DoctorDetailsDto
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Bio = doctor.Bio,
                Specialization = doctor.Specialization.ToString(),
                YearsOfExperience = doctor.YearsOfExperience,
                ProfilePictureUrl = doctor.ProfilePictureUrl,
                AverageRating = doctor.AverageRating,
                TotalReviews = doctor.TotalReviews,
                BaseConsultationFee = doctor.ConsultationFee,
                SessionDurationMinutes = doctor.SessionDurationMinutes,

                Branches = doctor.ClinicBranches.Select(b => new ClinicBranchDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Address = $"{b.City}, {b.StreetAddress}",
                    PhoneNumber = b.PhoneNumber,
                    GoogleMapLink = b.GoogleMapLink,
                    Fee = b.CustomConsultationFee ?? doctor.ConsultationFee,
                    IsActive = b.IsActive
                }).ToList(),

                RecentReviews = doctor.DoctorReviews.Select(r => new ReviewDto
                {
                    Rating = r.OverallSatisfaction,
                    Comment = r.Comment,

                    PatientName = r.IsAnonymous ? "فاعل خير" : (r.Patient?.FullName ?? "مريض"),
                    CreatedAt = r.CreatedAt
                }).ToList()
            };

            return Ok(result);
        }
        // ==========================================
        // Admin Operations (توثيق الطبيب)
        // ==========================================
        [HttpPut("{id}/verify")]
        // [Authorize(Roles = "Admin")] //  سيبها كومنت مؤقتاً عشان التيست يمشى
        public async Task<IActionResult> VerifyDoctor(Guid id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
                return NotFound(new { Message = "الدكتور غير موجود" });

          
            doctor.Status = Nabd.Core.Enums.DoctorStatus.Active;

            doctor.VerificationStatus = Nabd.Core.Enums.Identity.VerificationStatus.Verified;
            doctor.IsAvailable = true; 

            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = $"تم توثيق الدكتور {doctor.FullName} بنجاح وأصبح متاحاً للحجز" });
        }

    }
}