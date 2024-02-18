using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class RegistrationIncorrectException(string message) : ContactsNetException(message);
