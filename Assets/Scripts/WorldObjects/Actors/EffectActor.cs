using UnityEngine;

public abstract class EffectActor : ActorBase
{
    [field: SerializeField]
    private EffectSettings _effectSettings;
    protected Effect[] Effects { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (_effectSettings != null)
        {
            Effects = _effectSettings.GetEffects();
        }
    }

    public override void Act(WorldObject worldObject)
    {
        if (Effects != null && Effects.Length > 0)
        {
            Effects.Invoke(new CastState(WorldObject, WorldObject, worldObject));
        }
    }
}
