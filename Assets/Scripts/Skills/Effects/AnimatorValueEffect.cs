using UnityEngine;

public class AnimatorValueEffect : SimpleEffect
{
    [SerializeField]
    private AnimatorKey _key;

    public override void Invoke(CastState castState)
    {
        castState.Source.SetAnimatorValue(_key, Value);
    }
}
