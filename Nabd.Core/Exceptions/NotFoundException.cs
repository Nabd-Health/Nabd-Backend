using System.Net;

namespace Nabd.Core.Exceptions
{
  
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message)
          : base(message, HttpStatusCode.NotFound) 
        {
        }
    }
}