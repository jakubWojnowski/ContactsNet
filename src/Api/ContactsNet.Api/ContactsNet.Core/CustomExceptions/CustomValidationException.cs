using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class CustomValidationException(string message) : ContactsNetException(message);
