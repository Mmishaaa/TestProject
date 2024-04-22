using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace TestProject.Extensions
{
    public static class ErrorHandlingExstentions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();

                var exception = exceptionDetails?.Error;

                logger.LogError(
                    exception,
                    "The problem occured during processing a request on machine {Machine}. TraceId: {TraceId}",
                    Environment.MachineName,
                    Activity.Current?.Id);


                await Results.Problem(
                        title: "There is a problem, however we are working on it",
                        statusCode: StatusCodes.Status500InternalServerError,
                        extensions: new Dictionary<string, object?>
                        {
                           { "traceId", Activity.Current?.Id }
                        }
                    ).ExecuteAsync(context);
            });
        }
    }
}
