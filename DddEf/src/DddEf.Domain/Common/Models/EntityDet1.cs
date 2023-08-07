namespace DddEf.Domain.Common.Models;

public abstract class EntityDet1<TID> : IEquatable<EntityDet1<TID>> where TID : notnull
{
    public TID Det1Id { get; protected set; }

    public EntityDet1(TID det1Id)
    {
        Det1Id = det1Id;
    }
#pragma warning disable CS8618
    protected EntityDet1() 
    {
    }
#pragma warning disable CS8618
    public override bool Equals(object? obj)
    {
        return obj is EntityDet1<TID> entity && Det1Id.Equals(entity.Det1Id);
    }

    public bool Equals(EntityDet1<TID>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(EntityDet1<TID> left, EntityDet1<TID> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityDet1<TID> left, EntityDet1<TID> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return this.Det1Id.GetHashCode();
    }
}