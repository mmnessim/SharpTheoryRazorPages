namespace SharpTheory.Models;

public class HealthModel
{
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public HealthModel(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}