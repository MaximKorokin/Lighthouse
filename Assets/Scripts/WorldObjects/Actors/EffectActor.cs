using System.Collections.Generic;
using UnityEngine;

public abstract class EffectActor : ActorBase
{
    [field: SerializeField]
    private EffectSettings EffectSettings;

    private Effect[] _effects;
    protected IEnumerable<Effect> Effects => _effects;
    protected CastState CastState;

    protected override void Awake()
    {
        base.Awake();
        if (EffectSettings != null)
        {
            SetEffects(EffectSettings.GetEffects(), new CastState(WorldObject, EffectSettings.Cooldown));
        }
        else
        {
            SetEffects(new Effect[0], new CastState(WorldObject, 0));
        }
    }

    protected override void ActInternal(WorldObject worldObject)
    {
        if (_effects != null && _effects.Length > 0)
        {
            CastState.Target = worldObject;
            Effects.Invoke(CastState);
        }
    }

    public virtual void SetEffects(Effect[] effects, CastState castState)
    {
        CastState = castState;
        CastState.Source = WorldObject;
        _effects = effects;
    }
}
