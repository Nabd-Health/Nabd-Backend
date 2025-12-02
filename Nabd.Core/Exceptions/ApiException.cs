using System;
using System.Net; // لتعريف الـ Status Code

namespace Nabd.Core.Exceptions
{
    // Exception base class for all application errors meant to be returned to the client.
    public class ApiException : Exception
    {
        // الخاصية التي ستحدد الـ Status Code في الـ API Controller
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
          : base(message)
        {
            StatusCode = statusCode;
        }

        // لتمرير رسالة خطأ داخلية (Inner Exception)
        public ApiException(string message, Exception innerException)
      : base(message, innerException)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}