using UnityEngine;

public class ConfigBindable : Bindable
{
    [SerializeField]
    private ConfigKey _configKey;

    private void Start()
    {
        ValueBinder.SetValue(ConfigsManager.GetValue(_configKey));
    }

    protected override void OnValueChanged(object value)
    {
        ConfigsManager.SetValue(_configKey, value.ToString());
    }
}
