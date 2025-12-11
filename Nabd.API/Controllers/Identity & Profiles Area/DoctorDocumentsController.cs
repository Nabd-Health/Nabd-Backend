using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Doctors;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")] 
    public class DoctorDocumentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DoctorDocumentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private Guid GetCurrentDoctorId()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Guid.Empty;

            
            var doctor = _unitOfWork.Doctors.GetByEmailAsync(email).Result;
            return doctor?.Id ?? Guid.Empty;
        }

        // ==========================================
        // 1.  (GET)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> GetMyDocuments()
        {
            var doctorId = GetCurrentDoctorId();
            if (doctorId == Guid.Empty) return Unauthorized();

            var documents = await _unitOfWork.DoctorDocuments.GetByDoctorIdAsync(doctorId);
            var result = _mapper.Map<IEnumerable<DoctorDocumentResponseDto>>(documents);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            var doc = await _unitOfWork.DoctorDocuments.GetByIdAsync(id);
            if (doc == null) return NotFound("Document not found.");

            var doctorId = GetCurrentDoctorId();
            if (doc.DoctorId != doctorId) return Forbid(); 

            return Ok(_mapper.Map<DoctorDocumentResponseDto>(doc));
        }

        // ==========================================
        // 2.  (POST)
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doctorId = GetCurrentDoctorId();
            if (doctorId == Guid.Empty) return Unauthorized();

         
            var fakeUrl = $"https://nabd-storage.com/docs/{Guid.NewGuid()}_{dto.File.FileName}";


            var document = new DoctorDocument
            {
                DoctorId = doctorId,
                DocumentType = dto.DocumentType,
                FileUrl = fakeUrl,
                IsVerified = false, 
                UploadedAt = DateTime.UtcNow,
                Doctor = null! 
            };

            await _unitOfWork.DoctorDocuments.AddAsync(document);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<DoctorDocumentResponseDto>(document);
            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, response);
        }

        // ==========================================
        // 3.  (DELETE)
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var doc = await _unitOfWork.DoctorDocuments.GetByIdAsync(id);
            if (doc == null) return NotFound("Document not found.");

            var doctorId = GetCurrentDoctorId();
            if (doc.DoctorId != doctorId) return Forbid();

          
            if (doc.IsVerified)
                return BadRequest("Cannot delete a verified document.");

            _unitOfWork.DoctorDocuments.Delete(doc);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Document deleted successfully." });
        }
    }
}