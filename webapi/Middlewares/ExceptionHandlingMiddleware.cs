using webapi.Shared.Exceptions;

namespace webapi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException customEx)
        {
            context.Response.StatusCode = (int)customEx.StatusCode;
            await context.Response.WriteAsJsonAsync(customEx.ToResponse());
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(
                new CustomException("Ocurrió un error inesperado.")
                    .ToResponse()
            );
        }
    }
}
