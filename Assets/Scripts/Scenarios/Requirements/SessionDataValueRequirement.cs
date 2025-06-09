using UnityEngine;

public class SessionDataValueRequirement : ActRequirement
{
    [SerializeField]
    private SessionDataKey _sessionDataKey;

    private void Awake()
    {
        SessionDataStorage.Observable.SetChangeListener(_sessionDataKey, OnValueChanged);
    }

    private void OnValueChanged(string value)
    {
        if (ConvertingUtils.ToBool(value))
        {
            InvokeFulfilled();
        }
    }

    public override bool IsFulfilled()
    {
        return ConvertingUtils.ToBool(SessionDataStorage.Observable.Get(_sessionDataKey));
    }

    private void OnDestroy()
    {
        SessionDataStorage.Observable.RemoveChangeListener(_sessionDataKey, OnValueChanged);
    }

    public override string IconName => base.IconName;
}
