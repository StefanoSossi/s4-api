using Newtonsoft.Json;
using System.Net;
using s4.Logic.Exceptions;
using s4.Data.Exceptions;
using s4.Presentation.Exceptions;

namespace s4.Presentation.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private const string _jsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var ErrorResponse = new MiddlewareResponse<string>(null);
            if (ex is UnauthorizedAccessException || ex is ArgumentNullException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.Unauthorized;
                ErrorResponse.error.Message = "Unauthorized";
            }
            else if (ex is DatabaseException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.InternalServerError;
                ErrorResponse.error.Message = $"Data Error [Database] {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
            }
            else if (ex is BadRequestException exception)
            {
                ErrorResponse.Status = (int)HttpStatusCode.BadRequest;
                ErrorResponse.error.Message = $"Data Error [Bad Request]{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                if (exception.Details != null)
                {
                    ErrorResponse.error.Details = exception.Details;
                }
            }else if(ex is NotFoundException)
            {
                ErrorResponse.Status = (int)HttpStatusCode.NotFound;
                ErrorResponse.error.Message = $"Data Error [Not Found]{Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
            }
            else
            {
                ErrorResponse.Status = (int)HttpStatusCode.InternalServerError;
                ErrorResponse.error.Message = "Internal Server Error: "+ ex.Message;
            }
            context.Response.ContentType = _jsonContentType;
            context.Response.StatusCode = ErrorResponse.Status;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponse));
        }
    }
}