using System.Collections.Generic;

public class DataStore
{
    private readonly Dictionary<string, object> data = new();

    public void Set<T>(string key, T value)
    {
        data[key] = value;
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        if (data.TryGetValue(key, out var value))
        {
            if (value is T castedValue)
            {
                return castedValue;
            }
        }

        return defaultValue;
    }

    public bool TryGet<T>(string key, out T value)
    {
        if (data.TryGetValue(key, out var valueObject) && valueObject is T castedValue)
        {
            value = castedValue;
            return true;
        }

        value = default;
        return false;
    }

    public bool HasKey(string key) => data.ContainsKey(key);

    public bool Remove(string key) => data.Remove(key);

    public void Clear() => data.Clear();
}
