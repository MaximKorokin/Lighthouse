using UnityEngine;

public class CooldownCounter
{
    private float _lastUsedTime;

    public float Cooldown { get; set; }

    private float _cooldownDivider = 1;
    public float CooldownDivider
    {
        get => _cooldownDivider;
        set
        {
            if (value == 0)
            {
                Logger.Error($"{nameof(CooldownDivider)}'s value can't be 0");
                return;
            }
            _cooldownDivider = value;
        }
    }

    public float TimeSinceReset => Time.time - _lastUsedTime;


    public CooldownCounter(float cooldown)
    {
        Cooldown = cooldown;
        _lastUsedTime = float.MinValue;
    }

    public bool IsOver()
    {
        return TimeSinceReset >= Cooldown / CooldownDivider;
    }

    public bool TryReset()
    {
        var isOver = IsOver();
        if (isOver)
        {
            Reset();
        }
        return isOver;
    }

    public void Reset()
    {
        _lastUsedTime = Time.time;
    }
}
