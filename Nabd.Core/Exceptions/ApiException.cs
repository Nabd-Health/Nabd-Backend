using System;
using System.Net; 

namespace Nabd.Core.Exceptions
{
  
    public class ApiException : Exception
    {
      
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
          : base(message)
        {
            StatusCode = statusCode;
        }

       
        public ApiException(string message, Exception innerException)
      : base(message, innerException)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}