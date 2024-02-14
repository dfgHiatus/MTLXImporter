namespace MTLXImporter;

public class DataTransferObject<T, U>
{
    public T key;
    public U value;

    public DataTransferObject()
    {
        key = default;
        value = default;
    }

    public DataTransferObject(T key, U value)
    {
        this.key = key;
        this.value = value;
    }
}
