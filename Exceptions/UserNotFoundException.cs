namespace BookStoreApi.Exceptions;

public class UserNotFoundException : CustomException
{
    private const int ErrorCode = 404; // Not Found

    public UserNotFoundException() : base("User not found.", ErrorCode)
    {
    }

    public UserNotFoundException(string message,int errorCode) : base(message, ErrorCode)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, ErrorCode, innerException)
    {
    }
}