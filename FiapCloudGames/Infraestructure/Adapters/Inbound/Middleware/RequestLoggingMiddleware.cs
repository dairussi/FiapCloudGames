using System.Diagnostics;

namespace FiapCloudGames.Infraestructure.Adapters.Inbound.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("➡️ Request {method} {path}", context.Request.Method, context.Request.Path);

        await _next(context);

        stopwatch.Stop();
        _logger.LogInformation("⬅️ Response {statusCode} ({elapsed} ms)", context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}