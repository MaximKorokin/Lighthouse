using UnityEngine;

public class LogEffect : Effect
{
    [SerializeField]
    private LogLevel _logLevel;
    [SerializeField]
    private string _logString;

    public override void Invoke(CastState castState)
    {
        Logger.Write(_logString, _logLevel);
    }
}