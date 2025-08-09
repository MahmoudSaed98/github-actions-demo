namespace github_actions_demo.ConsoleApp.Core.Exceptions;

public sealed class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; } = null!;

    public ValidationException(IList<string> errors)
    {
        if (errors.Count == 0)
            throw new ArgumentNullException("Error list cannot be empty.", nameof(errors));

        this.Errors = errors;
    }

    public ValidationException() : base()
    {
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
