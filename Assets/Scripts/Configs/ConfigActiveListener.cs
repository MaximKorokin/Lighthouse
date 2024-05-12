public class ConfigActiveListener : ConfigValueListener
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(ConvertingUtils.ToBool(ConfigsManager.GetValue(Config)));
    }

    protected override void OnConfigValueChanged(object val)
    {
        gameObject.SetActive(ConvertingUtils.ToBool(val));
    }
}
