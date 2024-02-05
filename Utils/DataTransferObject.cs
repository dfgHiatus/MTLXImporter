namespace MTLXImporter;

internal class DataTransferObject<T, U>
{
    internal T key;
    internal U value;

    internal DataTransferObject()
    {
        key = default;
        value = default;
    }

    internal DataTransferObject(T key, U value)
    {
        this.key = key;
        this.value = value;
    }
}
