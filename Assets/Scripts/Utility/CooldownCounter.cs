using UnityEngine;

public class CooldownCounter
{
    private readonly float _cooldown;
    private float _lastUsedTime;

    public CooldownCounter(float cooldown)
    {
        _cooldown = cooldown;
        _lastUsedTime = Time.time - _cooldown;
    }

    public bool TryReset(float divider = 1)
    {
        if (divider == 0)
        {
            return false;
        }
        var isOver = Time.time - _lastUsedTime > _cooldown / divider;
        if (isOver)
        {
            _lastUsedTime = Time.time;
        }
        return isOver;
    }
}
