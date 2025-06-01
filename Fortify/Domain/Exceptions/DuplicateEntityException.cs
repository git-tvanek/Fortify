namespace Fortify.Domain.Exceptions;

public class DuplicateEntityException : DomainException
{
    public string EntityType { get; }
    public string PropertyName { get; }
    public string PropertyValue { get; }

    public DuplicateEntityException(string entityType, string propertyName, string propertyValue)
        : base($"{entityType} with {propertyName} '{propertyValue}' already exists.")
    {
        EntityType = entityType;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }
}