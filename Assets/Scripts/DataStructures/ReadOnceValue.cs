public class ReadOnceValue<T>
{
    private readonly T _defaultValue;
    private T _value;

    public ReadOnceValue(T defaultValue)
    {
        _defaultValue = defaultValue;
        _value = defaultValue;
    }

    public void Set(T value)
    {
        _value = value;
    }

    public static implicit operator T(ReadOnceValue<T> counter)
    {
        var returnValue = counter._value;
        counter._value = counter._defaultValue;
        return returnValue;
    }
}
