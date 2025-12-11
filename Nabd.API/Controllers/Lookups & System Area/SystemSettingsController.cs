using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabd.Application.DTOs.System; 
using Nabd.Core.Entities.System;
using Nabd.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.API.Controllers
{
    [Route("api/system-settings")] 
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class SystemSettingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemSettingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ==========================================
        // 1. عرض كل الإعدادات
        // ==========================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemParameter>>> GetAllSettings()
        {
            var settings = await _unitOfWork.SystemParameters.GetAllAsync();
            return Ok(settings);
        }

        // ==========================================
        // 2.  (Key)
        // ==========================================
        [HttpGet("{key}")]
        public async Task<ActionResult<SystemParameter>> GetSettingByKey(string key)
        {
            var setting = await _unitOfWork.SystemParameters.GetByKeyAsync(key);

            if (setting == null)
                return NotFound(new { Message = $"Setting with key '{key}' not found." });

            return Ok(setting);
        }

        // ==========================================
        // 3.  (Update Value)
        // ==========================================
        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateSetting(string key, [FromBody] UpdateSystemSettingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var setting = await _unitOfWork.SystemParameters.GetByKeyAsync(key);

            if (setting == null)
                return NotFound(new { Message = $"Setting with key '{key}' not found." });

            setting.Value = request.Value;

            
            if (!string.IsNullOrEmpty(request.Description))
            {
                setting.Description = request.Description;
            }

           

            _unitOfWork.SystemParameters.Update(setting);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "Setting updated successfully", Data = setting });
        }

        // ==========================================
        // 4. 
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> CreateSetting([FromBody] SystemParameter parameter)
        {
       
            var existing = await _unitOfWork.SystemParameters.GetByKeyAsync(parameter.Key);
            if (existing != null)
                return BadRequest(new { Message = $"Setting '{parameter.Key}' already exists." });

            await _unitOfWork.SystemParameters.AddAsync(parameter);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetSettingByKey), new { key = parameter.Key }, parameter);
        }
    }
}