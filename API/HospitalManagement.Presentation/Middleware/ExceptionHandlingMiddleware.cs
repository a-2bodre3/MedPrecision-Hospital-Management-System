using HospitalManagement.Domain.Exceptions;

namespace HospitalManagement.Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex) { await HandleExceptionAsync(context, ex); }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";


            context.Response.StatusCode = exception switch
            {
                UnauthorizedException => 401, 
                ForbiddenException => 403, 
                BadRequestException => 400,
                NotFoundException => 404,
                BusinessRuleException => 422,
                _ => 500  
            };

            var response = new { error = exception.Message };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
