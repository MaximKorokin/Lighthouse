using System;
using UnityEngine;

public abstract class ConfigValueBinder<T> : MonoBehaviour where T : IEquatable<T>
{
    [SerializeField]
    private ConfigKey _configKey;

    private T _previousValue;

    protected virtual void Start()
    {
        OnConfigChanged(ConfigsManager.Observable.Get(_configKey));
        ConfigsManager.Observable.SetChangeListener(_configKey, OnConfigChanged);
    }

    protected virtual void Update()
    {
        var currentValue = GetCurrentValue();
        if (!currentValue.Equals(_previousValue))
        {
            ConfigsManager.Observable.Set(_configKey, currentValue?.ToString());
        }
    }

    private void OnConfigChanged(string value)
    {
        _previousValue = ConvertToValue(value);
        SetValue(_previousValue);
    }

    private void OnDestroy()
    {
        ConfigsManager.Observable.RemoveChangeListener(_configKey, OnConfigChanged);
    }

    public abstract T GetCurrentValue();
    public abstract void SetValue(T obj);
    public abstract T ConvertToValue(string obj);
}
