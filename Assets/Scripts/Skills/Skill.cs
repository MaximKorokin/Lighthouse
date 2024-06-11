public class Skill
{
    private readonly Effect[] _effects;
    public readonly CooldownCounter CooldownCounter;

    private bool _inputRecieved;

    public Skill(EffectSettings settings)
    {
        _effects = settings.GetEffects();
        CooldownCounter = new CooldownCounter(settings.Cooldown);
        CooldownCounter.Reset();
    }

    public void Invoke(WorldObject source, float divider = 1) => Invoke(source, source, divider);

    public void Invoke(WorldObject source, WorldObject target, float divider = 1)
    {
        if (!_inputRecieved)
        {
            return;
        }
        _inputRecieved = false;

        if (!CooldownCounter.TryReset(divider))
        {
            return;
        }

        foreach (var effect in _effects)
        {
            effect.Invoke(new CastState(source, source, target));
        }
    }

    public void OnInputRecieved()
    {
        _inputRecieved = true;
    }
}
