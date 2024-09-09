using UnityEngine;

public class CooldownCounter
{
    private float _lastUsedTime;

    public float Cooldown { get; set; }
    public float TimeSinceReset => Time.time - _lastUsedTime;

    public CooldownCounter(float cooldown)
    {
        Cooldown = cooldown;
        _lastUsedTime = float.MinValue;
    }

    public bool IsOver(float divider = 1)
    {
        if (divider == 0)
        {
            return false;
        }
        return TimeSinceReset >= Cooldown / divider;
    }

    public bool TryReset(float divider = 1)
    {
        if (divider == 0)
        {
            return false;
        }
        var isOver = IsOver(divider);
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
