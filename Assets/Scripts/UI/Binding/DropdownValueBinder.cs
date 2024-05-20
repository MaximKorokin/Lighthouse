using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownValueBinder : ValueBinder
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    public override object GetCurrentValue()
    {
        return _dropdown.options[_dropdown.value].text;
    }

    public override void SetValue(object obj)
    {
        _dropdown.value = _dropdown.options.FindIndex(x => x.text == obj.ToString());
    }
}
