public class ConfigActiveListener : ConfigValueListener
{
    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(ConvertingUtils.ToBool(ConfigsManager.Observable.Get(Config)));
    }

    protected override void OnConfigValueChanged(object val)
    {
        gameObject.SetActive(ConvertingUtils.ToBool(val));
    }
}
