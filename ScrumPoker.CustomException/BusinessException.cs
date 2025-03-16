namespace ScrumPoker.CustomException;

public class BusinessException : Exception
{
    public BusinessException(Exception exception) : base(exception.Message)
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception inner) : base(message, inner)
    {
    }
}