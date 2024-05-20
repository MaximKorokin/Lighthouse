using UnityEngine;

public class ReferenceValueBinder : ValueBinder
{
    [SerializeField]
    private ValueBinder _binder;

    public override object GetCurrentValue()
    {
        return _binder.GetCurrentValue();
    }

    public override void SetValue(object obj)
    {
        _binder.SetValue(obj);
    }
}