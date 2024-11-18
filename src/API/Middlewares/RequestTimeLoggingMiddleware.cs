using System.Diagnostics;

namespace API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var startTime = Stopwatch.GetTimestamp();
        await next.Invoke(context);
        var deltatime = Stopwatch.GetElapsedTime(startTime);
        if (deltatime.TotalSeconds > 2)
        {
            logger.LogWarning("Request [{Verb}] ar {Path} took {Time} ms", context.Request.Method, context.Request.Path, deltatime);
        }

    }
}
