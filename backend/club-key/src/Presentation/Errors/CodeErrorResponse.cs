namespace Presentation.Errors;

public class CodeErrorResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public CodeErrorResponse(int statusCode, string message = null!)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageStatusCode(statusCode);
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => string.Empty
        };
    }
}