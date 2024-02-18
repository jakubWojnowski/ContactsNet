namespace ContactsNet.Core.CustomExceptions;

public class LoginFailedException(string message) : ContextMarshalException(message);