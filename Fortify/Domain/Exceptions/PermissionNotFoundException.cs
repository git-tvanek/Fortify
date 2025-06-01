namespace Fortify.Domain.Exceptions;

public class PermissionNotFoundException : DomainException
{
    public Guid? PermissionId { get; private set; }
    public string? PermissionName { get; private set; }

    public PermissionNotFoundException(Guid permissionId)
        : base($"Permission with ID '{permissionId}' was not found.")
    {
        PermissionId = permissionId;
    }

    public static PermissionNotFoundException ByName(string permissionName)
    {
        return new PermissionNotFoundException($"Permission with name '{permissionName}' was not found.", false)
        {
            PermissionName = permissionName
        };
    }

    private PermissionNotFoundException(string message, bool dummy) : base(message) { }
}