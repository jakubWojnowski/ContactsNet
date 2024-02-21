using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class ContactNotFoundException(string message) : ContactsNetException(message);