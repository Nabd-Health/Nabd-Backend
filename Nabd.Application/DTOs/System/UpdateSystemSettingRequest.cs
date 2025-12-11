using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.System
{
    public class UpdateSystemSettingRequest
    {
        [Required]
        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; } 
    }
}