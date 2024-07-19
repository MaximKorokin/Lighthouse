using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownValueBinder : ValueBinder<string>
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    public override string GetCurrentValue()
    {
        return _dropdown.options[_dropdown.value].text;
    }

    public override void SetValue(string value)
    {
        _dropdown.value = _dropdown.options.FindIndex(x => x.text == value);
    }

    public override string ConvertToValue(string str)
    {
        return str;
    }
}
