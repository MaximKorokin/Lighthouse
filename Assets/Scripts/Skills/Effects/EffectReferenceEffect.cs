using UnityEngine;

public class EffectReferenceEffect : Effect
{
    [SerializeField]
    private EffectSettings _effectSettings;

    public override void Invoke(CastState castState)
    {
        _effectSettings.GetEffects().Invoke(castState);
    }
}