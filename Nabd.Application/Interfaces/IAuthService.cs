using System;
using System.Threading.Tasks;

using Nabd.Core.DTOs; 

namespace Nabd.Application.Interfaces 
{
    public interface IAuthService
    {
        // 1. العمليات الأساسية
        Task<AuthResponseDto> RegisterDoctorAsync(object doctorDto);
        Task<AuthResponseDto> RegisterPatientAsync(object patientDto);
        Task<AuthResponseDto> LoginAsync(object loginDto);
        Task<AuthResponseDto> RenewTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string token);

        // 2. المميزات الإضافية
        Task<AuthResponseDto> GoogleLoginAsync(object googleDto, string? ipAddress);
        Task<AuthResponseDto> ForgotPasswordAsync(object forgotPasswordDto);
        Task<AuthResponseDto> VerifyResetOtpAndResetPasswordAsync(object resetPasswordDto);
        Task<AuthResponseDto> ChangePasswordAsync(Guid userId, object changePasswordDto);

        Task<AuthResponseDto> VerifyEmailAsync(object verifyEmailDto);
        Task<AuthResponseDto> ResendVerificationOtpAsync(object resendOtpDto);

        Task<AuthResponseDto> DeleteAccountAsync(Guid userId, object deleteAccountDto);
        Task<AuthResponseDto> DebugDeleteAccountByEmailAsync(object deleteAccountDto);

        // 3. إدارة البروفايل
        Task<object> GetDoctorProfileAsync(Guid userId);
        Task<object> GetPatientProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, object updateDto);
        Task<bool> ResendEmailVerificationAsync(Guid userId);
    }
}