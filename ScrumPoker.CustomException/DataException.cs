namespace ScrumPoker.CustomException;

public class DataException : Exception
{
    public DataException(Exception exception) : base(exception.Message)
    {
    }

    public DataException(string message) : base(message)
    {
    }

    public DataException(string message, Exception inner) : base(message, inner)
    {
    }
}