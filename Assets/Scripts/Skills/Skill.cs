public class Skill
{
    private readonly Effect[] _effects;
    private readonly CooldownCounter _cooldown;

    public Skill(EffectSettings settings)
    {
        _effects = settings.GetEffects();
        _cooldown = new CooldownCounter(settings.Cooldown);
        _cooldown.TryReset();
    }

    public void Invoke(WorldObject source) => Invoke(source, source);

    public void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var effect in _effects)
        {
            effect.Invoke(new CastState(source, source, target));
        }
    }

    public bool CanUse(float divider)
    {
        return _cooldown.TryReset(divider);
    }
}
