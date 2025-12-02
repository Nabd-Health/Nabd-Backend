using System.Net;

namespace Nabd.Core.Exceptions
{
    // Represents a resource not found (HTTP 404)
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message)
          : base(message, HttpStatusCode.NotFound) // 404 Not Found
        {
        }
    }
}