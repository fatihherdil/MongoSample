namespace MongoSample.Domain.Interfaces
{
    public interface INullableObject<T> where T:class
    {
        bool IsNull { get; set; }

        T GetNullInstance();
    }
}
