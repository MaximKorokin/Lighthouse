using UnityEngine;

public class SettingBindable : Bindable
{
    [SerializeField]
    private Setting _settingKey;

    private void Start()
    {
        ValueBinder.SetValue(SettingsManager.GetValue(_settingKey));
    }

    protected override void OnValueChanged(object value)
    {
        SettingsManager.SetValue(_settingKey, value.ToString());
    }
}
