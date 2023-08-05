namespace DddEf.Domain.Common.Models;

public abstract class Entity<TID> : IEquatable<Entity<TID>> where TID : notnull
{
    public TID Id { get; protected set; }

    public Entity(TID id)
    {
        Id = id;
    }
#pragma warning disable CS8618
    protected Entity() 
    {
    }
#pragma warning disable CS8618
    public override bool Equals(object? obj)
    {
        return obj is Entity<TID> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TID>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TID> left, Entity<TID> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TID> left, Entity<TID> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}