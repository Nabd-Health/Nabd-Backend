using System;
using System.Text.Json.Serialization;

namespace Nabd.Core.DTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }
        public Guid? UserId { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
    }
}