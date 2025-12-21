namespace Advent.Kernel;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

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
}
