using UnityEngine;

public abstract class EffectActor : ActorBase
{
    [field: SerializeField]
    protected EffectSettings EffectSettings;
    private Effect[] _effects;

    protected override void Awake()
    {
        base.Awake();
        if (EffectSettings != null)
        {
            _effects = EffectSettings.GetEffects();
        }
    }

    public override void Act(WorldObject worldObject)
    {
        if (_effects != null && _effects.Length > 0)
        {
            _effects.Invoke(new CastState(WorldObject, WorldObject, worldObject));
        }
    }
}
