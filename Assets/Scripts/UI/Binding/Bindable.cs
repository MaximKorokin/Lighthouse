using UnityEngine;

[RequireComponent(typeof(ValueBinder))]
public abstract class Bindable : MonoBehaviour
{
    protected ValueBinder ValueBinder { get; private set; }

    protected virtual void Awake()
    {
        ValueBinder = GetComponent<ValueBinder>();
        ValueBinder.ValueChanged += OnValueChanged;
    }

    protected abstract void OnValueChanged(object value);
}
