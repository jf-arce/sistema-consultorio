using System.Net;

namespace webapi.Shared.Exceptions;

public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public IEnumerable<object>? Errors { get; }

    public CustomException(HttpStatusCode statusCode, string message) : base(message) {
        StatusCode = statusCode;
    }

    public CustomException(string message) : base(message) {
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public CustomException(IEnumerable<object> errors) : base("Errores de validación")
    {
        StatusCode = HttpStatusCode.BadRequest;
        Errors = errors;
    }

    public object ToResponse()
    {
        return new
        {
            statusCode = (int)StatusCode,
            message = Message,
            errors = Errors
        };
    }
}
