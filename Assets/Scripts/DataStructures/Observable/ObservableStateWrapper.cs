using System.Collections.Generic;
using System.Linq;

public class ObservableStateWrapper<T> : ObservableKeyValueStoreWrapper<T, bool>
{
    private readonly Dictionary<T, bool> _items = new();

    protected override void OnSet(T key, bool value)
    {
        if (value)
        {
            _items[key] = true;
            _items.Keys.Except(key.Yield()).ToArray().For(x => Set(x, false));
        }
        else
        {
            _items[key] = false;
        }
    }

    protected override bool OnGet(T key)
    {
        return (_items.TryGetValue(key, out var value) && value) || false;
    }

    protected override bool HasKey(T key)
    {
        return _items.ContainsKey(key);
    }
}