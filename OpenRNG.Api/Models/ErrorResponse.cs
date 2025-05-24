namespace OpenRNG.Api.Models;

public class ErrorResponse
{
    public string Error { get; set; }
    public string Message { get; set; }
    public object? Parameters { get; set; }
}