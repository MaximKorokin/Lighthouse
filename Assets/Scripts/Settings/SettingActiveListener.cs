public class SettingActiveListener : SettingValueListener
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(ConvertingUtils.ToBool(SettingsManager.GetValue(Setting)));
    }

    protected override void OnSettingValueChanged(object val)
    {
        gameObject.SetActive(ConvertingUtils.ToBool(val));
    }
}
