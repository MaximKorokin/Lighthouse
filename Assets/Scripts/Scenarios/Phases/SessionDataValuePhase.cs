using UnityEngine;

public class SessionDataValuePhase : ActPhase
{
    [SerializeField]
    private SessionDataKey _key;
    [SerializeField]
    private string value;

    public override void Invoke()
    {
        SessionDataStorage.Observable.Set(_key, value);
        InvokeFinished();
    }

    public override string IconName => base.IconName;
    public override Color IconColor => ConvertingUtils.ToBool(value) ? MyColors.Green : MyColors.Red;
}
