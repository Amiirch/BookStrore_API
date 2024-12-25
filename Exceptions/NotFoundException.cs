namespace BookStoreApi.Exceptions;

public class NotFoundException: CustomException
{
    private const int ErrorCode = 404; // Not Found

    public NotFoundException() : base("not found.", ErrorCode)
    {
    }

    public NotFoundException(string message,int errorCode) : base(message, ErrorCode)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, ErrorCode, innerException)
    {
    }
}