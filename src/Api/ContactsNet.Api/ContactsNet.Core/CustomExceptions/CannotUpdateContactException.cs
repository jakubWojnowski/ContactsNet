using ContactsNet.Core.Exceptions;

namespace ContactsNet.Core.CustomExceptions;

public class CannotUpdateContactException(Guid id) : ContactsNetException($"Contact with id {id} cannot be updated");