using System;

namespace EnterpriseCraft.Template.Shared.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
