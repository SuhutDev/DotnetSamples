namespace DddEf.Domain.Common.Models;
public abstract class AggregateRoot<TID> : Entity<TID> where TID : notnull
{
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning disable CS8618
    public AggregateRoot(TID id) : base(id)
    {
    } 
}