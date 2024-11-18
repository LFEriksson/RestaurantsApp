namespace Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentitifier) : Exception($"{resourceType} with id: {resourceIdentitifier} dosen't exist")
{
}
