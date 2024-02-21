using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class CannotAddContactException(string email)
    : ContactsNetException($"Cannot add contact with email {email} because it is  user's email.");
