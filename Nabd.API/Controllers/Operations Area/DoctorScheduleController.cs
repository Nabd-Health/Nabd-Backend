using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.Operations; // CreateDoctorScheduleRequest, DoctorScheduleResponse
using Nabd.Core.Entities.Operations;
using Nabd.Core.Interfaces;
using System.Security.Claims;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")] // 🔒 المنطقة دي للدكاترة فقط
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ==========================================
        // Helper: جلب ID الدكتور الحالي من التوكن
        // ==========================================
        private async Task<Guid?> GetCurrentDoctorIdAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return null;

            var doctor = await _unitOfWork.Doctors.GetByEmailAsync(email);
            return doctor?.Id;
        }

        // ==========================================
        // 1. عرض الجدول (Read)
        // ==========================================
        [HttpGet("my-schedule")]
        public async Task<IActionResult> GetMySchedule()
        {
            var doctorId = await GetCurrentDoctorIdAsync();
            if (doctorId == null) return Unauthorized();

            var schedules = await _unitOfWork.DoctorSchedules.GetByDoctorIdAsync(doctorId.Value);
            var result = _mapper.Map<IEnumerable<DoctorScheduleResponse>>(schedules);

            return Ok(result);
        }

        // ==========================================
        // 2. إضافة موعد عمل جديد (Create)
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] CreateDoctorScheduleRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doctorId = await GetCurrentDoctorIdAsync();
            if (doctorId == null) return Unauthorized();

            // 1. التحقق من أن الدكتور يمتلك هذا الفرع
            var branch = await _unitOfWork.ClinicBranches.GetByIdAsync(dto.ClinicBranchId);
            if (branch == null)
                return NotFound("الفرع غير موجود.");

            if (branch.DoctorId != doctorId)
                return Forbid("لا يمكنك إضافة جدول لفرع لا تملكه.");

            // 2. التحقق من التضارب (Overlap Check) - الميزة الذكية
            var hasConflict = await _unitOfWork.DoctorSchedules.HasOverlappingScheduleAsync(
                doctorId.Value, dto.DayOfWeek, dto.StartTime, dto.EndTime);

            if (hasConflict)
                return BadRequest("يوجد تعارض في المواعيد. لديك جدول آخر في نفس التوقيت.");

            // 3. الحفظ
            var schedule = _mapper.Map<DoctorSchedule>(dto);
            schedule.DoctorId = doctorId.Value;

            await _unitOfWork.DoctorSchedules.AddAsync(schedule);
            await _unitOfWork.CompleteAsync();

            // إرجاع الـ DTO بعد الحفظ (عشان الـ ID الجديد)
            var response = _mapper.Map<DoctorScheduleResponse>(schedule);
            return CreatedAtAction(nameof(GetMySchedule), new { id = schedule.Id }, response);
        }

        // ==========================================
        // 3. حذف موعد عمل (Delete)
        // ==========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            var doctorId = await GetCurrentDoctorIdAsync();
            if (doctorId == null) return Unauthorized();

            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (schedule == null) return NotFound("الجدول غير موجود.");

            // Security: التأكد إن الجدول يخص الدكتور ده
            if (schedule.DoctorId != doctorId)
                return Forbid("لا يمكنك حذف جدول لا تملكه.");

            _unitOfWork.DoctorSchedules.Delete(schedule);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "تم حذف الموعد بنجاح" });
        }
    }
}