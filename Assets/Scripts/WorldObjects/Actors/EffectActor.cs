using System.Collections.Generic;
using UnityEngine;

public abstract class EffectActor : ActorBase
{
    [SerializeField]
    private EffectSettings _effectSettings;

    private Effect[] _effects;
    protected IEnumerable<Effect> Effects => _effects;
    protected CastState CastState;

    protected override void Awake()
    {
        base.Awake();
        if (_effectSettings != null)
        {
            SetEffects(_effectSettings.GetEffects(), new CastState(WorldObject));
        }
        else
        {
            SetEffects(new Effect[0], new CastState(WorldObject));
        }
    }

    protected override void ActInternal(WorldObject worldObject)
    {
        if (_effects == null || _effects.Length == 0)
        {
            return;
        }
        base.ActInternal(worldObject);
        CastState.Target = worldObject;
        _effects.Invoke(CastState);
    }

    public virtual void SetEffects(Effect[] effects, CastState castState)
    {
        CastState = castState;
        CastState.Source = WorldObject;
        _effects = effects;
    }
}
