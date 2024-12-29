using System.Collections.Generic;
using System;

public abstract class ObservableKeyValueStoreWrapper<K, V> where V : IEquatable<V>
{
    private static readonly Dictionary<K, HashSet<Action<V>>> _listeners = new();

    public void Set(K key, V value)
    {
        if (!OnGet(key).Equals(value) && _listeners.TryGetValue(key, out var listeners))
        {
            listeners.ForEach(x => x(value));
        }
        OnSet(key, value);
    }

    /// <summary>
    /// Will use <see cref="DefaultValueAttribute"/> to obtain default value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public V Get(K key)
    {
        // extraction
        if (!HasKey(key))
        {
            var defaultValue = key.GetDefaultValue();
            var result = defaultValue is not null and V d ? d : default;
            OnSet(key, result);
            return result;
        }
        else
        {
            return OnGet(key);
        }
    }

    public void SetChangeListener(K key, Action<V> action)
    {
        if (_listeners.TryGetValue(key, out var listeners))
        {
            listeners.Add(action);
        }
        else
        {
            _listeners[key] = new(action.Yield());
        }
    }

    public void RemoveChangeListener(K key, Action<V> action)
    {
        if (_listeners.TryGetValue(key, out var actions))
        {
            actions.Remove(action);
        }
    }

    protected abstract void OnSet(K key, V value);
    protected abstract V OnGet(K key);
    protected abstract bool HasKey(K key);
}