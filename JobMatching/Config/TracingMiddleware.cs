using System.Diagnostics;

namespace JobMatching.Config
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ActivitySource ActivitySource = new("Tracing.API");

        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<TracingMiddleware> logger)
        {
            // Cria um Activity para a requisição  
            using var activity = ActivitySource.StartActivity("HTTP Request", ActivityKind.Server);

            if (activity != null)
            {
                activity.SetTag("http.method", context.Request.Method);
                activity.SetTag("http.url", context.Request.Path);
                activity.SetTag("client.ip", context.Connection.RemoteIpAddress?.ToString());
            }

            var traceId = activity?.TraceId.ToString() ?? Guid.NewGuid().ToString();

            // Log inicial  
            logger.LogInformation("Iniciando requisição. TraceId={TraceId}", traceId);

            // Retorna TraceId no header da resposta  
            context.Response.Headers.Add("trace-id", traceId);

            await _next(context);

            logger.LogInformation("Finalizando requisição. TraceId={TraceId}", traceId);
        }
    }

}
