using UnityEngine;

public class ConfigValuePhase : ActPhase
{
    [SerializeField]
    private ConfigKey _key;
    [SerializeField]
    private string value;

    public override void Invoke()
    {
        ConfigsManager.SetValue(_key, value);
        InvokeFinished();
    }

    public override string IconName => "Config.png";
}