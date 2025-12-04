using System;
using System.Threading.Tasks;
using Nabd.Core.DTOs; // ✅ تأكد من وجود ده

namespace Nabd.Core.Interfaces
{
    public interface IAuthService
    {
        // ✅ التعديل: Return Type أصبح AuthResponseDto ليطابق الـ Service
        Task<AuthResponseDto> RegisterDoctorAsync(object doctorDto);
        Task<AuthResponseDto> RegisterPatientAsync(object patientDto);
        Task<AuthResponseDto> LoginAsync(object loginDto);
        Task<AuthResponseDto> RenewTokenAsync(string refreshToken);

        Task<bool> RevokeTokenAsync(string token);

        Task<object> GetDoctorProfileAsync(Guid userId);
        Task<object> GetPatientProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, object updateDto);
        Task<bool> ResendEmailVerificationAsync(Guid userId);
    }
}