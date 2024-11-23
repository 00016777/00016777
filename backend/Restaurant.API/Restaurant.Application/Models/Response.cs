namespace Restaurant.Application.Models;

public class Response
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public static class Statuses
{
    public static string Error = "Error";

    public static string Success = "Success";
}
