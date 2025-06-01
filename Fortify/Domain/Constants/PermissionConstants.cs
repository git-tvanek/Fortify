namespace Fortify.Domain.Constants;

public static class PermissionConstants
{
    public const string UserView = "Users.View";
    public const string UserCreate = "Users.Create";
    public const string UserEdit = "Users.Edit";
    public const string UserDelete = "Users.Delete";
    public const string UserBlock = "Users.Block";
    public const string UserUnblock = "Users.Unblock";
    public const string UserManageRoles = "Users.ManageRoles";

    public const string RoleView = "Roles.View";
    public const string RoleCreate = "Roles.Create";
    public const string RoleEdit = "Roles.Edit";
    public const string RoleDelete = "Roles.Delete";
    public const string RoleManagePermissions = "Roles.ManagePermissions";

    public const string PermissionView = "Permissions.View";
    public const string PermissionCreate = "Permissions.Create";
    public const string PermissionEdit = "Permissions.Edit";
    public const string PermissionDelete = "Permissions.Delete";

    public const string SystemAdministration = "System.Administration";
    public const string SystemConfiguration = "System.Configuration";
    public const string SystemLogs = "System.Logs";
    public const string SystemAudit = "System.Audit";
}