namespace ScrumPoker.Common.Models;

public class UiMessageModel
{
    public UiMessageModel()
    {

    }

    public UiMessageModel(string message, string type)
    {
        Message = message;
        Type = type;
    }

    public string Message { get; set; }
    public string Type { get; set; }
}