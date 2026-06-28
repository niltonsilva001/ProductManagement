using FluentValidation;

namespace ProductManagement.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errorResponse = new Models.ErrorResponse
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = "Validation failed",
                Errors = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage))
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var errorResponse = new Models.ErrorResponse
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = "Resource not found",
                Errors = ex.Message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new Models.ErrorResponse
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = "An unexpected error occurred.",
                Errors = ex.Message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}