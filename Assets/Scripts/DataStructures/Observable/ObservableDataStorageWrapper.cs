using System.Collections.Generic;

public class ObservableDataStorageWrapper<T> : ObservableKeyValueStoreWrapper<T, string>
{
    private readonly Dictionary<T, string> _data = new();

    protected override bool HasKey(T key)
    {
        return _data.ContainsKey(key);
    }

    protected override string OnGet(T key)
    {
        return _data.TryGetValue(key, out var value) ? value : "";
    }

    protected override void OnSet(T key, string value)
    {
        _data[key] = value;
    }
}
