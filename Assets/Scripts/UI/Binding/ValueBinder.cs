using System;
using UnityEngine;

public abstract class ValueBinder<T> : MonoBehaviour where T : IEquatable<T>
{
    [SerializeField]
    private ConfigKey _configKey;

    private T _previousValue;

    protected virtual void Start()
    {
        _previousValue = ConvertToValue(ConfigsManager.GetValue(_configKey));
        SetValue(_previousValue);
        ConfigsManager.SetChangeListener(_configKey, OnConfigChanged);
    }

    protected virtual void Update()
    {
        var currentValue = GetCurrentValue();
        if (!currentValue.Equals(_previousValue))
        {
            ConfigsManager.SetValue(_configKey, currentValue?.ToString());
        }
    }

    private void OnConfigChanged(string value)
    {
        _previousValue = ConvertToValue(value);
        SetValue(_previousValue);
    }

    private void OnDestroy()
    {
        ConfigsManager.RemoveChangeListener(_configKey, OnConfigChanged);
    }

    public abstract T GetCurrentValue();
    public abstract void SetValue(T obj);
    public abstract T ConvertToValue(string obj);
}
