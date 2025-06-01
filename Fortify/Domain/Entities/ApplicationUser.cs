using Fortify.Domain.Common;
using Fortify.Domain.Constants;
using Fortify.Domain.Enums;
using Fortify.Domain.Events;
using Fortify.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Fortify.Domain.Entities;

public class ApplicationUser : IdentityUser, IHasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];
    private readonly List<ApplicationRole> _roles = [];
    private readonly List<RefreshToken> _refreshTokens = [];

    public AccountStatus AccountStatus { get; private set; } = AccountStatus.Active;
    public DateTime DateCreated { get; private set; }
    public DateTime? DateModified { get; private set; }
    public bool Is2FAEnabled { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public DateTime? LastPasswordChangeDate { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEndDate { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string? PreferredLanguage { get; private set; }
    public string? TimeZone { get; private set; }

    public virtual IReadOnlyCollection<ApplicationRole> Roles => _roles.AsReadOnly();
    public virtual IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected ApplicationUser() { }

    public ApplicationUser(Username username, Email email, string? firstName = null, string? lastName = null)
    {
        Id = Guid.NewGuid().ToString();
        UserName = username;
        NormalizedUserName = username.Value.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.Value.ToUpperInvariant();
        FirstName = firstName;
        LastName = lastName;
        DateCreated = DateTime.UtcNow;
        SecurityStamp = Guid.NewGuid().ToString();

        AddDomainEvent(new UserRegisteredEvent
        {
            UserId = Id,
            Username = UserName,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            RegisteredAt = DateCreated
        });
    }

    public void UpdateProfile(string? firstName, string? lastName, string? preferredLanguage, string? timeZone)
    {
        FirstName = firstName;
        LastName = lastName;
        PreferredLanguage = preferredLanguage;
        TimeZone = timeZone;
        DateModified = DateTime.UtcNow;
    }

    public void UpdateProfilePicture(string? profilePictureUrl)
    {
        ProfilePictureUrl = profilePictureUrl;
        DateModified = DateTime.UtcNow;
    }

    public void RecordLogin()
    {
        LastLoginDate = DateTime.UtcNow;
        FailedLoginAttempts = 0;
        DateModified = DateTime.UtcNow;
    }

    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;
        DateModified = DateTime.UtcNow;

        if (FailedLoginAttempts >= DefaultValues.MaxLoginAttempts)
        {
            LockoutEndDate = DateTime.UtcNow.AddMinutes(DefaultValues.LockoutDurationMinutes);
        }
    }

    public void ResetFailedLoginAttempts()
    {
        FailedLoginAttempts = 0;
        LockoutEndDate = null;
        DateModified = DateTime.UtcNow;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        LastPasswordChangeDate = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserPasswordChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            ChangedAt = DateTime.UtcNow,
            WasReset = false
        });
    }

    public void ResetPassword(string passwordHash, string? resetBy = null)
    {
        PasswordHash = passwordHash;
        LastPasswordChangeDate = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserPasswordChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            ChangedAt = DateTime.UtcNow,
            WasReset = true
        });
    }

    public void Enable2FA()
    {
        Is2FAEnabled = true;
        TwoFactorEnabled = true;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new User2FAEnabledEvent
        {
            UserId = Id,
            Username = UserName!,
            EnabledAt = DateTime.UtcNow
        });
    }

    public void Disable2FA()
    {
        Is2FAEnabled = false;
        TwoFactorEnabled = false;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new User2FADisabledEvent
        {
            UserId = Id,
            Username = UserName!,
            DisabledAt = DateTime.UtcNow
        });
    }

    public void Block(string? reason = null, string? blockedBy = null)
    {
        var oldStatus = AccountStatus;
        AccountStatus = AccountStatus.Blocked;
        LockoutEnabled = true;
        LockoutEnd = DateTimeOffset.MaxValue;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserBlockedEvent
        {
            UserId = Id,
            Username = UserName!,
            Reason = reason,
            BlockedBy = blockedBy,
            BlockedAt = DateTime.UtcNow
        });

        AddDomainEvent(new UserAccountStatusChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            OldStatus = oldStatus,
            NewStatus = AccountStatus,
            Reason = reason,
            ChangedBy = blockedBy,
            ChangedAt = DateTime.UtcNow
        });
    }

    public void Unblock(string? unblockedBy = null)
    {
        var oldStatus = AccountStatus;
        AccountStatus = AccountStatus.Active;
        LockoutEnabled = false;
        LockoutEnd = null;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserUnblockedEvent
        {
            UserId = Id,
            Username = UserName!,
            UnblockedBy = unblockedBy,
            UnblockedAt = DateTime.UtcNow
        });

        AddDomainEvent(new UserAccountStatusChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            OldStatus = oldStatus,
            NewStatus = AccountStatus,
            ChangedBy = unblockedBy,
            ChangedAt = DateTime.UtcNow
        });
    }

    public void Deactivate(string? reason = null, string? deactivatedBy = null)
    {
        var oldStatus = AccountStatus;
        AccountStatus = AccountStatus.Deactivated;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserDeactivatedEvent
        {
            UserId = Id,
            Username = UserName!,
            Reason = reason,
            DeactivatedBy = deactivatedBy,
            DeactivatedAt = DateTime.UtcNow
        });

        AddDomainEvent(new UserAccountStatusChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            OldStatus = oldStatus,
            NewStatus = AccountStatus,
            Reason = reason,
            ChangedBy = deactivatedBy,
            ChangedAt = DateTime.UtcNow
        });
    }

    public void Reactivate(string? reactivatedBy = null)
    {
        var oldStatus = AccountStatus;
        AccountStatus = AccountStatus.Active;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new UserReactivatedEvent
        {
            UserId = Id,
            Username = UserName!,
            ReactivatedBy = reactivatedBy,
            ReactivatedAt = DateTime.UtcNow
        });

        AddDomainEvent(new UserAccountStatusChangedEvent
        {
            UserId = Id,
            Username = UserName!,
            OldStatus = oldStatus,
            NewStatus = AccountStatus,
            ChangedBy = reactivatedBy,
            ChangedAt = DateTime.UtcNow
        });
    }

    public void AddRole(ApplicationRole role, string? assignedBy = null)
    {
        if (_roles.Any(r => r.Id == role.Id))
            return;

        _roles.Add(role);
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new RoleAssignedEvent
        {
            UserId = Id,
            Username = UserName!,
            RoleId = role.Id,
            RoleName = role.Name!,
            AssignedBy = assignedBy,
            AssignedAt = DateTime.UtcNow
        });
    }

    public void RemoveRole(ApplicationRole role, string? removedBy = null)
    {
        if (!_roles.Any(r => r.Id == role.Id))
            return;

        _roles.RemoveAll(r => r.Id == role.Id);
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new RoleRemovedEvent
        {
            UserId = Id,
            Username = UserName!,
            RoleId = role.Id,
            RoleName = role.Name!,
            RemovedBy = removedBy,
            RemovedAt = DateTime.UtcNow
        });
    }

    public RefreshToken AddRefreshToken(string token, string? createdByIp = null)
    {
        var refreshToken = new RefreshToken
        {
            Token = token,
            CreatedByIp = createdByIp,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(DefaultValues.RefreshTokenExpiryDays)
        };

        _refreshTokens.Add(refreshToken);
        DateModified = DateTime.UtcNow;

        return refreshToken;
    }

    public void RevokeRefreshToken(string token, string? revokedByIp = null, string? replacedByToken = null)
    {
        var refreshToken = _refreshTokens.FirstOrDefault(rt => rt.Token == token && rt.IsActive);
        if (refreshToken != null)
        {
            refreshToken.Revoke(revokedByIp, replacedByToken);
            DateModified = DateTime.UtcNow;
        }
    }

    public void RemoveExpiredRefreshTokens()
    {
        _refreshTokens.RemoveAll(rt => !rt.IsActive && rt.ExpiresAt < DateTime.UtcNow.AddDays(-1));
        DateModified = DateTime.UtcNow;
    }

    public void AddDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}