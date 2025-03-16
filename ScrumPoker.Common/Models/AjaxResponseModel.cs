namespace ScrumPoker.Common.Models;

public class AjaxResponseModel
{
    public object? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
}