using System.Net;

namespace TwitterStreamExceptionHandling
{
    public class DataNotFoundValidationException : Exception, IValidationException
    {
        public DataNotFoundValidationException()
        {

        }
        public DataNotFoundValidationException(string message) : base(message)
        {
            
        }
        public HttpStatusCode StatusCode { get => HttpStatusCode.NotFound;  }
        public string DefaultMessage { get => "Data not found"; }
    }
}