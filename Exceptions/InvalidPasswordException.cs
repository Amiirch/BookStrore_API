namespace BookStoreApi.Exceptions;

public class InvalidPasswordException : CustomException
{
    private const int ErrorCode = 401; // Unauthorized

    public InvalidPasswordException() : base("Invalid password.", ErrorCode)
    {
    }

    public InvalidPasswordException(string message, int errorCode) : base(message, ErrorCode)
    {
    }

    public InvalidPasswordException(string message, Exception innerException) : base(message, ErrorCode, innerException)
    {
    }
}