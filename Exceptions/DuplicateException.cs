namespace BookStoreApi.Exceptions;

public class DuplicateException : CustomException
{
    private const int ErrorCode = 409;
    
    public DuplicateException() : base("Invalid password.", ErrorCode)
    {
    }

    public DuplicateException(string message, int errorCode) : base(message, ErrorCode)
    {
    }

    public DuplicateException(string message, Exception innerException) : base(message, ErrorCode, innerException)
    {
    }
}