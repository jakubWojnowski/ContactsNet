using System.Net;

namespace ContactsNet.Core.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);