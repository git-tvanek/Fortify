namespace Fortify.Domain.Common;

public interface IAuditable
{
    DateTime DateCreated { get; }
    DateTime? DateModified { get; }
    string? CreatedBy { get; }
    string? ModifiedBy { get; }
}