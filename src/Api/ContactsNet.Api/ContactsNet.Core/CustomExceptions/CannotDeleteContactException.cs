using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class CannotDeleteContactException(Guid id) : ContactsNetException($"Contact with id {id} cannot be deleted");
