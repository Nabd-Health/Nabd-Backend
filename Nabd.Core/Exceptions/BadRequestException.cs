using System.Net;

namespace Nabd.Core.Exceptions
{
    // Represents an invalid request from the client (HTTP 400)
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message)
          : base(message, HttpStatusCode.BadRequest) // 400 Bad Request
        {
        }
    }
}