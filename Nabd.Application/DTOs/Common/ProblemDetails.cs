namespace Nabd.Application.DTOs.Common
{
    public class ProblemDetails
    {
        public int StatusCode { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; } // مسار الـ Request (Path)

        // تفاصيل إضافية (مثل Validation Errors)
        public object? Extensions { get; set; }

        public ProblemDetails(int statusCode, string? title = null, string? detail = null, string? instance = null)
        {
            StatusCode = statusCode;
            Title = title ?? GetDefaultMessageForStatusCode(statusCode);
            Detail = detail;
            Instance = instance;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side",
                _ => "An unexpected error occurred"
            };
        }
    }
}