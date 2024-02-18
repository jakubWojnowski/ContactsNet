namespace ContactsNet.Core.Exceptions;

public interface IExceptionResponseMapper
{
    ExceptionResponse? Map(Exception exception);
}