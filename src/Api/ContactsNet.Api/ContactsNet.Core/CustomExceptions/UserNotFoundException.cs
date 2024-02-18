using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class UserNotFoundException(string message) : ContactsNetException(message);