namespace BookStoreApi.Exceptions;

public class CustomException: Exception
{
    public int ErrorCode { get; private set; }

    public CustomException(string message, int errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public CustomException(string message, int errorCode, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
    
}
