using System.Collections;
using System.Collections.Generic;

public class DataStore<K> : IEnumerable<KeyValuePair<K, object>>
{
    private readonly Dictionary<K, object> data = new();

    public void Set<T>(K key, T value)
    {
        data[key] = value;
    }

    public T Get<T>(K key, T defaultValue = default)
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

    public bool TryGet<T>(K key, out T value)
    {
        if (data.TryGetValue(key, out var valueObject) && valueObject is T castedValue)
        {
            value = castedValue;
            return true;
        }

        value = default;
        return false;
    }

    public bool HasKey(K key) => data.ContainsKey(key);

    public bool Remove(K key) => data.Remove(key);

    public void Clear() => data.Clear();

    public IEnumerator<KeyValuePair<K, object>> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
