using UnityEngine;

public class CooldownCounter
{
    public readonly float Cooldown;
    private float _lastUsedTime;

    public float TimeSinceReset => Time.time - _lastUsedTime;

    public CooldownCounter(float cooldown)
    {
        Cooldown = cooldown;
        _lastUsedTime = Time.time - Cooldown;
    }

    public bool TryReset(float divider = 1)
    {
        if (divider == 0)
        {
            return false;
        }
        var isOver = TimeSinceReset > Cooldown / divider;
        if (isOver)
        {
            _lastUsedTime = Time.time;
        }
        return isOver;
    }
}
