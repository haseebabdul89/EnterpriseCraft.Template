using System;
using EnterpriseCraft.Template.Shared.Common;

namespace EnterpriseCraft.Template.Modules.Customers.Domain;

public class Customer : AuditableEntity
{
    // Expose the inherited Id for convenience (read-only to preserve aggregate identity rules)
    public new Guid Id => base.Id;

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }

    // Map to inherited auditable properties so we don't duplicate storage
    public DateTimeOffset CreatedOn
    {
        get => CreatedAt;
        set => CreatedAt = value;
    }

    public DateTimeOffset? ModifiedOn
    {
        get => ModifiedAt;
        set => ModifiedAt = value;
    }
    public int CreatedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}

