using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class CannotAddContactException(string email)
    : ContactsNetException($"Contact with email {email} already exists");
