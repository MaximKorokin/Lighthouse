using UnityEngine;

public class Skill
{
    private readonly Effect[] _effects;
    private readonly float _cooldown;
    private float _lastUsedTime;

    public Skill(EffectSettings settings)
    {
        _effects = settings.GetEffects();
        _cooldown = settings.Cooldown;
        _lastUsedTime = Time.time;
    }

    public void Invoke(WorldObject source) => Invoke(source, source);

    public void Invoke(WorldObject source, WorldObject target)
    {
        _lastUsedTime = Time.time;
        foreach (var effect in _effects)
        {
            effect.Invoke(new CastState(source, source, target));
        }
    }

    public bool CanUse(float divider)
    {
        return Time.time - _lastUsedTime > _cooldown / divider;
    }

}
