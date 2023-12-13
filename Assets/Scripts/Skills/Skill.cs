public class Skill
{
    private readonly Effect[] _effects;
    private readonly CooldownCounter _cooldownCounter;

    public Skill(EffectSettings settings)
    {
        _effects = settings.GetEffects();
        _cooldownCounter = new CooldownCounter(settings.Cooldown);
        _cooldownCounter.TryReset();
    }

    public void Invoke(WorldObject source) => Invoke(source, source);

    public void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var effect in _effects)
        {
            effect.Invoke(new CastState(source, source, target, _cooldownCounter.Cooldown));
        }
    }

    public bool CanUse(float divider = 1)
    {
        return _cooldownCounter.TryReset(divider);
    }
}
