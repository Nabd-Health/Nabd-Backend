using Microsoft.AspNetCore.Mvc;
using Nabd.Application.Interfaces;
using Nabd.Core.DTOs;
using Nabd.Core.Interfaces;

namespace Nabd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ==========================================
        // 1.  (Registration)
        // ==========================================

      
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

       
            var result = await _authService.RegisterDoctorAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result); 

            return Ok(result); 
        }

     
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterPatientAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // ==========================================
        // 2.  (Login)
        // ==========================================

      
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsSuccess)
                return Unauthorized(result); 

            return Ok(result);
        }

        // ==========================================
        // 3. (Session Management)
        // ==========================================

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { Message = "Refresh Token مطلوب" });

            var result = await _authService.RenewTokenAsync(refreshToken);

            if (!result.IsSuccess)
                return Unauthorized(result); 

            return Ok(result);
        }

     
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { Message = "Token is required" });

            var result = await _authService.RevokeTokenAsync(refreshToken);

            if (!result)
                return BadRequest(new { Message = "Invalid token" });

            return Ok(new { Message = "تم تسجيل الخروج بنجاح" });
        }
    }
}