namespace ApplicationCore.Entity
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; protected set; }
    }
}
