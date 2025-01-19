public class ConfigActiveSetter : ObservableListener<ConfigKey, string>
{
    protected override ObservableKeyValueStoreWrapper<ConfigKey, string> Observable => ConfigsManager.Observable;

    protected override void OnObservableValueChanged(string val)
    {
        gameObject.SetActive(ConvertingUtils.ToBool(val));
    }
}
