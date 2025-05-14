using UnityEngine;

public class AnimatorEffect : Effect
{
    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    public override void Invoke(CastState castState)
    {
        var target = castState.GetTarget();
        if (target.TryGetComponent(out Animator animator) ||
            target.transform.GetChild(0).TryGetComponent(out animator))
        {
            animator.runtimeAnimatorController = _animatorController;
        }
        else
        {
            Logger.Warn($"Target object {target} doesn't contain {typeof(Animator)}");
        }
    }
}
