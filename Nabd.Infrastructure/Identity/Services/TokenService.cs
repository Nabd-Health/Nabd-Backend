using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nabd.Core.Entities;
using Nabd.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Nabd.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Nabd.Infrastructure.Repositories;
using Nabd.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Nabd.Infrastructure.Identity.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IGenericRepository<RefreshToken> _refreshTokenRepo;

		public TokenService(IConfiguration config, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_config = config;
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_refreshTokenRepo = _unitOfWork.Repository<RefreshToken>();
		}

		// ==========================================
		// 1. CreateToken: توليد التوكن الرئيسي (JWT)
		// ==========================================
		public string CreateToken(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.UserType.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]!));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = credentials,
				Issuer = _config["Token:Issuer"]!
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}

		// ==========================================
		// 2. GenerateRefreshToken: توليد Refresh Token (آمن)
		// ==========================================
		// ⚠️ تم تعديل Signature الدالة لـ Guid appUserId, string ipAddress
		public async Task<RefreshToken> GenerateRefreshToken(Guid appUserId, string ipAddress)
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);

			var refreshToken = new RefreshToken
			{
				// الخصائص الـ required
				AppUserId = appUserId,
				Token = Convert.ToBase64String(randomNumber),
				// الخصائص الـ BaseEntity
				RowVersion = Array.Empty<byte>(),
				ExpiresOn = DateTime.UtcNow.AddDays(30),
				CreatedOn = DateTime.UtcNow,
				CreatedByIp = ipAddress,
			};
			return await Task.FromResult(refreshToken);
		}

		// ==========================================
		// 3. RenewTokenAsync (تجديد الجلسة) - تم التصحيح
		// ==========================================
		public async Task<object> RenewTokenAsync(string refreshToken)
		{
			var tokenSpec = new RefreshTokenByTokenSpec(refreshToken);

			var tokens = await _refreshTokenRepo.ListAsync(tokenSpec);
			var storedToken = tokens.FirstOrDefault();

			if (storedToken == null || storedToken.IsActive == false)
			{
				if (storedToken?.ReplacedByToken != null)
				{
					await RevokeDescendantRefreshTokens(storedToken, storedToken.CreatedByIp!, "Attempted reuse of revoked ancestor token");
				}
				throw new SecurityTokenException("Invalid refresh token or token already used.");
			}

			var user = await _unitOfWork.Repository<AppUser>().GetByIdAsync(storedToken.AppUserId);
			var newJwtToken = CreateToken(user);

			// ⚠️ تمرير AppUserId للتوكن الجديد
			var newRefreshToken = await GenerateRefreshToken(user.Id, storedToken.CreatedByIp!);

			storedToken.RevokedOn = DateTime.UtcNow;
			storedToken.ReplacedByToken = newRefreshToken.Token;
			_refreshTokenRepo.Update(storedToken);

			// نستخدم NewRefreshToken مباشرة في الـ Add
			_refreshTokenRepo.Add(newRefreshToken);

			await _unitOfWork.CompleteAsync();

			return new { JwtToken = newJwtToken, RefreshToken = newRefreshToken.Token };
		}

		// ==========================================
		// 4. RevokeTokenAsync (الخروج الآمن) - تم التصحيح
		// ==========================================
		public async Task<bool> RevokeTokenAsync(string token)
		{
			var tokenSpec = new RefreshTokenByTokenSpec(token);

			var tokens = await _refreshTokenRepo.ListAsync(tokenSpec);
			var storedToken = tokens.FirstOrDefault();

			if (storedToken == null || storedToken.IsActive == false)
				return false;

			storedToken.RevokedOn = DateTime.UtcNow;
			storedToken.ReasonRevoked = "Manual logout";
			storedToken.RevokedByIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

			_refreshTokenRepo.Update(storedToken);
			await _unitOfWork.CompleteAsync();

			return true;
		}

		// ==========================================
		// 5. ReplaceRefreshToken (تنفيذ العقد CS0535)
		// ==========================================
		public async Task<RefreshToken> ReplaceRefreshToken(string token, string ipAddress, string reason)
		{
			// ⚠️ هذا هو التنفيذ الناقص الذي يرضي الـ Interface (سيكمل لاحقاً)
			throw new NotImplementedException();
		}


		// ==========================================
		// 6. Helper Methods (لتحليل التوكن)
		// ==========================================

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = _config["Token:Issuer"]!,
				ValidateAudience = false,
				ValidateLifetime = false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]!))
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}
			return principal;
		}

		private async Task RevokeDescendantRefreshTokens(RefreshToken token, string ipAddress, string reason)
		{
			await Task.CompletedTask;
		}

        public Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}