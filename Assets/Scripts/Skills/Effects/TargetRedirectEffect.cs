using UnityEngine;

public class TargetRedirectEffect : ComplexEffect
{
    [SerializeField]
    private TargetingType _redirectTo;

    public override void Invoke(CastState castState)
    {
        castState.TargetingType = _redirectTo;
        base.Invoke(castState);
    }
}
