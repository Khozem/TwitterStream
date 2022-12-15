using System.Net;

namespace TwitterStreamExceptionHandling
{
    public interface IValidationException
    {
        public HttpStatusCode StatusCode { get; }
        public string DefaultMessage { get; }
    }
}
