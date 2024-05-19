using System.Collections.Generic;
using UnityEngine;

public class UndoableEffectActor : EffectActor
{
    [SerializeField]
    private EffectSettings _reverseEffectSettings;

    private Effect[] _reverseEffects;
    protected IEnumerable<Effect> ReverseEffects => _reverseEffects;

    protected override void Awake()
    {
        base.Awake();
        if (_reverseEffectSettings != null)
        {
            SetReverseEffects(_reverseEffectSettings.GetEffects());
        }
        else
        {
            SetReverseEffects(new Effect[0]);
        }
    }

    public override void Idle(WorldObject worldObject)
    {
        if (_reverseEffects == null || _reverseEffects.Length == 0)
        {
            return;
        }
        CastState.Target = worldObject;
        _reverseEffects.Invoke(CastState);
    }

    public virtual void SetReverseEffects(Effect[] effects)
    {
        CastState.Source = WorldObject;
        _reverseEffects = effects;
    }
}
