using System;

namespace Nabd.Core.Entities.Base
{
    public interface SoftDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        string? DeletedBy { get; set; }
    }
}