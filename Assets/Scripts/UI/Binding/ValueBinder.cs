using System;
using UnityEngine;

public abstract class ValueBinder : MonoBehaviour
{
    protected object PreviousValue;

    public event Action<object> ValueChanged;

    protected virtual void Update()
    {
        var currentValue = GetCurrentValue();
        if (!currentValue.Equals(PreviousValue)) 
        {
            PreviousValue = currentValue;
            ValueChanged?.Invoke(PreviousValue);
        }
    }

    public abstract object GetCurrentValue();
    public abstract void SetValue(object obj);
}
