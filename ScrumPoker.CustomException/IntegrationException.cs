namespace ScrumPoker.CustomException;

public class IntegrationException : Exception
{
    public IntegrationException(Exception exception) : base(exception.Message)
    {
    }

    public IntegrationException(string message) : base(message)
    {
    }

    public IntegrationException(string message, Exception inner) : base(message, inner)
    {
    }
}