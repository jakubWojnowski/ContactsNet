using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.Middlewares;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse? Map(Exception exception);
}