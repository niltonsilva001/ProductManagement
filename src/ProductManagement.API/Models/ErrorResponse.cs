namespace ProductManagement.API.Models;

public class ErrorResponse
{
    public string StatusCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Errors { get; set; } = string.Empty;
}