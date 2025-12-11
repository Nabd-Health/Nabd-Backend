using Nabd.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.System
{
    public class SystemParameter : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Key { get; set; } 

        [Required]
        [MaxLength(500)]
        public required string Value { get; set; } 

        [MaxLength(200)]
        public string? Description { get; set; } 

        public string? Group { get; set; } 
    }
}