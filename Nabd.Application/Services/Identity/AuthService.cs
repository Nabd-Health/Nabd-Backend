using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // مهم عشان نستخدم دوال EF
using Nabd.Core.DTOs; // تأكد إن الـ DTOs موجودة
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Application.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        // =========================================================
        // 1. التسجيل (Registration)
        // =========================================================

        public async Task<AuthResponseDto> RegisterDoctorAsync(object doctorDto)
        {
            // Casting الآمن
            var dto = doctorDto as RegisterDoctorDto ?? throw new ArgumentException("Invalid DTO Type: Expected RegisterDoctorDto");

            if (await UserExists(dto.Email))
                return new AuthResponseDto { IsSuccess = false, Message = "البريد الإلكتروني مسجل بالفعل." };

            var user = CreateBaseUser(dto.Email, dto.FirstName, dto.LastName, dto.PhoneNumber, UserType.Doctor);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    return new AuthResponseDto { IsSuccess = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

                await _userManager.AddToRoleAsync(user, "Doctor");

                // إنشاء بروفايل الطبيب بكامل التفاصيل
                var doctor = new Doctor
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    FullName = $"{dto.FirstName} {dto.LastName}",
                    Specialization = dto.Specialization,
                    ConsultationFee = dto.ConsultationFee,

                    // دمجنا اسم العيادة مع العنوان
                    Address = $"{dto.ClinicName} - {dto.ClinicAddress}",
                    City = dto.City,

                    // البيانات الإضافية
                    MedicalLicenseNumber = dto.MedicalLicenseNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Bio = dto.Bio,

                    Status = DoctorStatus.Pending,
                    IsAvailable = true // متاح افتراضياً
                };

                await _unitOfWork.Doctors.AddAsync(doctor);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                // يفضل استخدام ILogger هنا
                return new AuthResponseDto { IsSuccess = false, Message = $"حدث خطأ أثناء التسجيل: {ex.Message}" };
            }
        }

        public async Task<AuthResponseDto> RegisterPatientAsync(object patientDto)
        {
            var dto = patientDto as RegisterPatientDto ?? throw new ArgumentException("Invalid DTO Type: Expected RegisterPatientDto");

            if (await UserExists(dto.Email))
                return new AuthResponseDto { IsSuccess = false, Message = "البريد الإلكتروني مسجل بالفعل." };

            var user = CreateBaseUser(dto.Email, dto.FirstName, dto.LastName, dto.PhoneNumber, UserType.Patient);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    return new AuthResponseDto { IsSuccess = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };

                await _userManager.AddToRoleAsync(user, "Patient");

                var patient = new Patient
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    FullName = $"{dto.FirstName} {dto.LastName}",
                    NationalId = dto.NationalId,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,

                    // بيانات الـ AI الاختيارية
                    BloodType = dto.BloodType ?? Nabd.Core.Enums.Medical.BloodType.Unknown,
                    ChronicDiseases = dto.ChronicDiseases,
                    Allergies = dto.Allergies
                };

                await _unitOfWork.Patients.AddAsync(patient);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new AuthResponseDto { IsSuccess = false, Message = $"حدث خطأ أثناء التسجيل: {ex.Message}" };
            }
        }

        // =========================================================
        // 2. الدخول (Login)
        // =========================================================

        public async Task<AuthResponseDto> LoginAsync(object loginDto)
        {
            var dto = loginDto as LoginDto ?? throw new ArgumentException("Invalid DTO Type: Expected LoginDto");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return new AuthResponseDto { IsSuccess = false, Message = "البريد الإلكتروني أو كلمة المرور غير صحيحة." };

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return new AuthResponseDto { IsSuccess = false, Message = "البريد الإلكتروني أو كلمة المرور غير صحيحة." };

            // تحديث تاريخ آخر ظهور
            user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return await GenerateAuthResponseAsync(user);
        }

        // =========================================================
        // 3. إدارة التوكن والأمان
        // =========================================================

        public async Task<AuthResponseDto> RenewTokenAsync(string refreshToken)
        {
            var storedToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);

            if (storedToken == null || !storedToken.IsActive)
                return new AuthResponseDto { IsSuccess = false, Message = "التوكن غير صالح أو منتهي." };

            // Token Rotation: إلغاء القديم
            storedToken.RevokedOn = DateTime.UtcNow;
            storedToken.ReasonRevoked = "Renewed by Token Rotation";

            // استخدام الـ Repository الخاص بنا للتحديث
            _unitOfWork.RefreshTokens.Update(storedToken);
            await _unitOfWork.CompleteAsync();

            var user = await _userManager.FindByIdAsync(storedToken.AppUserId.ToString());
            return await GenerateAuthResponseAsync(user!);
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            await _unitOfWork.RefreshTokens.RevokeTokenAsync(token, "Manual Logout");
            return await _unitOfWork.CompleteAsync() > 0;
        }

        // =========================================================
        // 4. إدارة الملف الشخصي (Helpers)
        // =========================================================

        public async Task<object> GetDoctorProfileAsync(Guid userId)
            => await _unitOfWork.Doctors.GetByIdWithDetailsAsync(userId) ?? throw new Exception("Doctor profile not found");

        public async Task<object> GetPatientProfileAsync(Guid userId)
            => await _unitOfWork.Patients.GetByIdWithDetailsAsync(userId) ?? throw new Exception("Patient profile not found");

        // Placeholder for future implementation
        public Task<bool> UpdateProfileAsync(Guid userId, object updateDto) => Task.FromResult(true);
        public Task<bool> ResendEmailVerificationAsync(Guid userId) => Task.FromResult(true);

        // =========================================================
        // Private Helpers
        // =========================================================

        private async Task<bool> UserExists(string email)
            => await _userManager.FindByEmailAsync(email) != null;

        private AppUser CreateBaseUser(string email, string fName, string lName, string phone, UserType type)
        {
            return new AppUser
            {
                Email = email,
                UserName = email, // استخدام الإيميل كـ Username
                FirstName = fName,
                LastName = lName,
                PhoneNumber = phone,
                UserType = type,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                EmailConfirmed = false // حتى يتم التفعيل
            };
        }

        private async Task<AuthResponseDto> GenerateAuthResponseAsync(AppUser user)
        {
            var token = _tokenService.CreateToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id, "N/A"); // IP مؤقتاً

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                UserId = user.Id,
                Email = user.Email!,
                UserType = user.UserType.ToString(),
                DisplayName = user.DisplayName
            };
        }
    }
}