namespace ScrumPoker.CustomException;

public class UIException : Exception
{
    public UIException(Exception exception) : base(exception.Message)
    {
    }

    public UIException(string message) : base(message)
    {
    }

    public UIException(string message, Exception inner) : base(message, inner)
    {
    }
}