using System.Collections.Generic;
using System.Linq;

public class ObservableStateWrapper<T> : ObservableKeyValueStoreWrapper<T, bool>
{
    private readonly Dictionary<T, bool> _items = new();
    private readonly HashSet<T> _forbiddenStates = new();

    protected override void OnSet(T key, bool value)
    {
        if (value)
        {
            _items[key] = true;
            // Reset other state values if not forbidden
            foreach (var item in _items.Keys.Except(key.Yield()).ToArray())
            {
                if (!_forbiddenStates.Contains(item)) Set(item, false);
            }
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

    public void ControlStateReset(T key, bool allow)
    {
        if (allow) _forbiddenStates.Remove(key);
        else _forbiddenStates.Add(key);
    }
}
