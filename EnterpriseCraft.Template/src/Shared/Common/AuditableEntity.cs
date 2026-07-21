using System;

namespace EnterpriseCraft.Template.Shared.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
}
