public class ConfigActiveSetter : ObservableListener<ConfigKey, string>
{
    protected override ObservableKeyValueStoreWrapper<ConfigKey, string> Observable => ConfigsManager.Observable;

    protected override void Awake()
    {
        base.Awake();
        // If component is created in runtime
        // it's Start is called before first Update.
        // It means that it will be visible for one frame.
        gameObject.SetActive(false);
    }

    protected override void OnObservableValueChanged(string val)
    {
        gameObject.SetActive(ConvertingUtils.ToBool(val));
    }
}
