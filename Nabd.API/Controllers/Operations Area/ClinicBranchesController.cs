using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Operations; // Ensure this namespace exists
using Nabd.Core.DTOs; // Or wherever CreateClinicBranchRequest is
using Nabd.Core.Entities.Operations;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")] // Only doctors can manage their clinics
    public class ClinicBranchesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClinicBranchesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ==========================================
        // 1. Read Operations (عرض الفروع)
        // ==========================================

        [HttpGet("my-branches")]
        public async Task<IActionResult> GetMyBranches()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);
            if (doctor == null) return BadRequest("Doctor profile not found.");

            var branches = await _unitOfWork.ClinicBranches.GetBranchesByDoctorIdAsync(doctor.Id);
            var result = _mapper.Map<IEnumerable<ClinicBranchResponse>>(branches);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Patients need to see branch details too
        public async Task<IActionResult> GetBranch(Guid id)
        {
            var branch = await _unitOfWork.ClinicBranches.GetByIdAsync(id);
            if (branch == null) return NotFound(new { Message = "Branch not found" });

            var result = _mapper.Map<ClinicBranchResponse>(branch);
            return Ok(result);
        }

        // ==========================================
        // 2. Create & Update (إدارة الفروع)
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] CreateClinicBranchRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);

            var branch = _mapper.Map<ClinicBranch>(dto);
            branch.DoctorId = doctor!.Id;

            await _unitOfWork.ClinicBranches.AddAsync(branch);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetBranch), new { id = branch.Id }, new { Message = "Branch created successfully", BranchId = branch.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(Guid id, [FromBody] CreateClinicBranchRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var branch = await _unitOfWork.ClinicBranches.GetByIdAsync(id);
            if (branch == null) return NotFound("Branch not found");

            // Security: Ensure doctor owns this branch
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
            if (branch.DoctorId != doctor!.Id) return Forbid();

            // Update fields (Mapper can do this, or manual)
            _mapper.Map(dto, branch);

            _unitOfWork.ClinicBranches.Update(branch);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Branch updated successfully" });
        }

        // ==========================================
        // 3. Delete (حذف)
        // ==========================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            var branch = await _unitOfWork.ClinicBranches.GetByIdAsync(id);
            if (branch == null) return NotFound();

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email!);
            if (branch.DoctorId != doctor!.Id) return Forbid();

            _unitOfWork.ClinicBranches.Delete(branch);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Branch deleted successfully" });
        }

        // ==========================================
        // 4. Clinic Images (صور العيادة) - Future
        // ==========================================
        // Note: We need a FileService to implement this properly.
        // For now, we will place a placeholder endpoint.

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadBranchImage(Guid id, IFormFile image)
        {
            // 1. Check Branch existence & Ownership
            // 2. Upload file to server/cloud
            // 3. Save URL to database (ClinicBranch needs a 'Photos' collection or similar)

            return Ok(new { Message = "Image upload logic pending FileService implementation" });
        }
    }
}