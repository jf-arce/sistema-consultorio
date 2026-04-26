using Microsoft.AspNetCore.Mvc;
using webapi.Shared.Exceptions;

namespace webapi.Extensions;

public static class ApiBehaviorExtensions
{
    public static IServiceCollection AddCustomApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .SelectMany(x =>
                    {
                        if (x.Value == null)
                            return Enumerable.Empty<object>();

                        return x.Value.Errors.Select(e => new
                        {
                            field = x.Key,
                            message = e.ErrorMessage
                        });
                    });

                var ex = new CustomException(errors);

                return new BadRequestObjectResult(ex.ToResponse());
            };
        });

        return services;
    }
}
