using System.Diagnostics.CodeAnalysis;

namespace Advent.Kernel;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    [MemberNotNullWhen(true, nameof(DeletedAt))]
    public bool IsDeleted { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }

    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Entity ent)
        {
            return false;
        }

        return Id == ent.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    protected void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    protected void Recover()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
