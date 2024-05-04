using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleValueBinder : ValueBinder
{
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    public override object GetCurrentValue()
    {
        return _toggle.isOn;
    }

    public override void SetValue(object obj)
    {
        _toggle.isOn = ConvertingUtils.ToBool(obj);
    }
}
