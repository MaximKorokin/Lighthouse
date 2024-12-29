using UnityEngine;

public class ConfigValuePhase : ActPhase
{
    [SerializeField]
    private ConfigKey _key;
    [SerializeField]
    private string value;

    public override void Invoke()
    {
        ConfigsManager.Observable.Set(_key, value);
        InvokeFinished();
    }

    public override string IconName => "Config.png";
    public override Color IconColor => ConvertingUtils.ToBool(value) ? MyColors.Green : MyColors.Red;
}
