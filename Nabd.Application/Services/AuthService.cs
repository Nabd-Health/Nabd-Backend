using Nabd.Core.Interfaces;
using Nabd.Core.Entities;
using Nabd.Core.Enums;
using Nabd.Core.Specifications;
using Nabd.Infrastructure.Repositories;
// ⚠️ FIX: تحديد الـ Namespaces الصحيحة للـ DTOs
using Nabd.Application.DTOs.Auth;
using Nabd.Application.DTOs.Doctor;
using Nabd.Application.DTOs.Patient;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; // لحل خطأ FirstOrDefaultAsync

namespace Nabd.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // ==========================================
        // 1. التسجيل (Registration) - [دكتور]
        // ==========================================
        public async Task<object> RegisterDoctorAsync(object doctorDto)
        {
            var registerDto = (RegisterDoctorDto)doctorDto;

            var userCheckSpec = new UserByEmailSpec(registerDto.Email);
            // FIX CS1061: استخدام GetEntityWithSpec
            var existingUser = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(userCheckSpec);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email address is already registered.");
            }

            var user = _mapper.Map<AppUser>(registerDto);
            user.UserType = UserType.Doctor;
            user.UserName = registerDto.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var doctor = _mapper.Map<Doctor>(registerDto);

            user.DoctorProfile = doctor;
            _unitOfWork.Repository<AppUser>().Add(user);
            await _unitOfWork.CompleteAsync();

            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "N/A";
            var jwtToken = _tokenService.CreateToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id, ipAddress);

            user.RefreshTokens.Add(refreshToken);
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.UserType.ToString(),
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                FullName = user.DisplayName
            };
        }

        // ==========================================
        // 2. الدخول (Login) - Core Authentication
        // ==========================================
        public async Task<object> LoginAsync(object loginDto)
        {
            var login = (LoginDto)loginDto;

            var userSpec = new UserByEmailSpec(login.Email, true);
            var user = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(userSpec); // ⬅️ FIX CS1061

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            user.LastLoginDate = DateTime.UtcNow;
            _unitOfWork.Repository<AppUser>().Update(user);

            var jwtToken = _tokenService.CreateToken(user);
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "N/A";
            var refreshToken = await _tokenService.GenerateRefreshToken(user.Id, ipAddress);

            user.RefreshTokens.Add(refreshToken);
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.UserType.ToString(),
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                FullName = user.DisplayName
            };
        }

        // ... (باقي الدوال المطلوبة في العقد) ...

        public async Task<object> RegisterPatientAsync(object patientDto) { throw new NotImplementedException(); }
        public async Task<object> GetDoctorProfileAsync(Guid userId) { throw new NotImplementedException(); }
        public async Task<object> GetPatientProfileAsync(Guid userId) { throw new NotImplementedException(); }
        public async Task<bool> UpdateProfileAsync(Guid userId, object updateDto) { throw new NotImplementedException(); }
        public async Task<object> RenewTokenAsync(string refreshToken) { throw new NotImplementedException(); }
        public async Task<bool> RevokeTokenAsync(string token) { throw new NotImplementedException(); }
        public async Task<bool> ResendEmailVerificationAsync(Guid userId) { throw new NotImplementedException(); }
    }
}