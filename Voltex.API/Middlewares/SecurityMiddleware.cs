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
            context.Response.Headers["X-Content-Type-Options"] = "nonsniff";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'none'";
            context.Response.Headers["Referrer-Policy"] = "no-referrer";

            var Route = context.GetRouteData().Values["controller"];
            if (Route is not null && Route.ToString() == "Account")
            {
                context.Response.Headers["Cache-Control"] = "no-store";
            }

                await  _next(context);
        }
    }
}
