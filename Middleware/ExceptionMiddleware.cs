using System.Text.Json;

namespace Hospital.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware (RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception )
            {
                var response = new
                {
                    Succes = false,
                    Message = "Something went wrong"
                };
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
