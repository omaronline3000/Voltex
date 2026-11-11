namespace Voltex.API.Middlewares
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Security Headers
            context.Response.Headers["X-Message-From-Developer"] = "Have a nice day";
            await  _next(context);
        }
    }
}
