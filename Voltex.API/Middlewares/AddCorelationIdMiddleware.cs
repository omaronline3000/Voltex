using Serilog.Context;

namespace Voltex.API.Middlewares
{
    public class AddCorelationIdMiddleware
    {
        public RequestDelegate _next;

        public AddCorelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            var corelationId = 
                Guid.TryParse(context.Request.Headers["X-Corelation-Id"], out Guid id) ? 
                id.ToString() : 
                Guid.NewGuid().ToString();

            context.Response.Headers["X-Corelation-Id"] = corelationId;

            using(LogContext.PushProperty("CorleationId" , corelationId))
            {
                await _next(context);
            }

        }
    }
}
