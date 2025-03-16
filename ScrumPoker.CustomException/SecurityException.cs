namespace ScrumPoker.CustomException;

public class SecurityException : Exception
{
    public SecurityException(Exception exception) : base(exception.Message)
    {
    }

    public SecurityException(string message) : base(message)
    {
    }

    public SecurityException(string message, Exception inner) : base(message, inner)
    {
    }
}