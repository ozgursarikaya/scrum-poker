namespace ScrumPoker.CustomException;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionDto> Errors { get; }

    public ValidationException() : base()
    {
        Errors = Array.Empty<ValidationExceptionDto>();
    }

    public ValidationException(string? message) : base(message)
    {
        Errors = Array.Empty<ValidationExceptionDto>();
    }


    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionDto>();
    }

    public ValidationException(IEnumerable<ValidationExceptionDto> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    private static string BuildErrorMessage(IEnumerable<ValidationExceptionDto> errors)
    {
        IEnumerable<string> err = errors.Select(x => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}");
        return $"Validation failed: {string.Join(string.Empty, err)}";
    }
}

public class ValidationExceptionDto
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}