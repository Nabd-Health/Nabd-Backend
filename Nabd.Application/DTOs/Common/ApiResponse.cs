using System.Collections.Generic;

namespace Nabd.Application.DTOs.Common
{
    // كلاس عام لتوحيد شكل الردود (Wrapper)
    // أي رد من الـ API سيكون بهذا الشكل: { succeeded: true, data: { ... }, message: "..." }
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        // 1. كونستركتور فارغ (مهم للـ Serialization)
        public ApiResponse() { }

        // 2. كونستركتور النجاح (Success)
        public ApiResponse(T data, string? message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }

        // 3. كونستركتور الفشل (Failure)
        public ApiResponse(string message, List<string>? errors = null)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }
    }
}