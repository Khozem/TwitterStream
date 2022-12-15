using System.Net;

namespace TwitterStreamExceptionHandling
{
    public class BusinessValidationException : Exception, IValidationException
    {
        public BusinessValidationException()
        {

        }
        public BusinessValidationException(string message) : base(message)
        {
            
        }
        public HttpStatusCode StatusCode { get => HttpStatusCode.BadRequest;  }
        public string DefaultMessage { get => "Business validation failure"; }
    }
}