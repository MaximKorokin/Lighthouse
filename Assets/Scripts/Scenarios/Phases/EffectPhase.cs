using UnityEngine;

public class EffectPhase : ActPhase
{
    [SerializeField]
    private EffectSettings _effectSettings;
    [field: SerializeField]
    public WorldObject WorldObject { get; private set; }

    private Effect[] _effects;

    private void Awake()
    {
        _effects = _effectSettings.GetEffects();
    }

    public override void Invoke()
    {
        _effects.Invoke(new CastState(WorldObject));
        InvokeFinished();
    }

    public override string IconName => "Effect.png";
}
