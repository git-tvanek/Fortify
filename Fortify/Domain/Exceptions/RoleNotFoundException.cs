﻿namespace Fortify.Domain.Exceptions;

public class RoleNotFoundException : DomainException
{
    public string? RoleId { get; private set; }
    public string? RoleName { get; private set; }

    public RoleNotFoundException(string roleId)
        : base($"Role with ID '{roleId}' was not found.")
    {
        RoleId = roleId;
    }

    public static RoleNotFoundException ByName(string roleName)
    {
        return new RoleNotFoundException($"Role with name '{roleName}' was not found.", false)
        {
            RoleName = roleName
        };
    }

    private RoleNotFoundException(string message, bool dummy) : base(message) { }
}