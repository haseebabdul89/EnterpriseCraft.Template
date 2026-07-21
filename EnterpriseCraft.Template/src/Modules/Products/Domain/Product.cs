using System;
using EnterpriseCraft.Template.Shared.Common;

namespace EnterpriseCraft.Template.Modules.Products.Domain;

public class Product : AuditableEntity
{
    // Expose the inherited Id for convenience (read-only to preserve aggregate identity rules)
    public Guid Id => base.Id;

    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsDeleted { get; set; }

    // Map to inherited auditable properties to avoid duplicating state
    public DateTimeOffset CreatedOn
    {
        get => CreatedAt;
        set => CreatedAt = value;
    }

    public int CreatedBy { get; set; }

    public DateTimeOffset? ModifiedOn
    {
        get => ModifiedAt;
        set => ModifiedAt = value;
    }

    public int? ModifiedBy { get; set; }
}