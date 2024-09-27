using UnityEngine;

public class AnimatorValueEffect : SimpleValueEffect
{
    [SerializeField]
    private AnimatorKey _key;

    public override void Invoke(CastState castState)
    {
        castState.Source.SetAnimatorValue(_key, Value);
    }
}
