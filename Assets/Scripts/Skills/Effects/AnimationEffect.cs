using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "ScriptableObjects/Effects/AnimationEffect", order = 1)]
public class AnimationEffect : Effect
{
    [SerializeField]
    private AnimationClip _animation;

    public override void Invoke(CastState castState)
    {
        castState.Source.StartCoroutine(AnimationCoroutine(castState));
    }

    private IEnumerator AnimationCoroutine(CastState castState)
    {
        var animator = GenericAnimatorPool.Instance.Take(_animation);
        animator.transform.position = castState.Target.transform.position;
        yield return new WaitForSeconds(_animation.length);
        GenericAnimatorPool.Instance.Return(animator);
    }
}
