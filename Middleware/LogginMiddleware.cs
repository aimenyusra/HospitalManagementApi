namespace Hospital.Middleware
{
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;
        public LogginMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            Console.WriteLine($"Request Started : {context.Request.Method} {context.Request.Path}");
            await _next(context);
            Console.WriteLine($"Request Sent : {context.Response.StatusCode}");
        }
    }
}
