using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "ScriptableObjects/Effects/AnimationEffect", order = 1)]
public class AnimationEffect : Effect
{
    [SerializeField]
    private AnimationClip _animation;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private bool _childToTarget;
    [SerializeField]
    private bool _flipWithTarget;

    public override void Invoke(CastState castState)
    {
        castState.Source.StartCoroutine(AnimationCoroutine(castState));
    }

    private IEnumerator AnimationCoroutine(CastState castState)
    {
        var animator = GenericAnimatorPool.Take(_animation);
        if (_childToTarget)
        {
            animator.transform.parent = castState.Target.transform;
        }
        animator.transform.position = castState.Target.transform.position;
        if (_flipWithTarget)
        {
            var targetSpriteRenderer = castState.Target.GetComponent<SpriteRenderer>();
            var animSpriteRenderer = animator.GetComponent<SpriteRenderer>();
            if (targetSpriteRenderer != null && animSpriteRenderer != null)
            {
                animSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
        }
        yield return new WaitForSeconds(_duration > 0 ? _duration : _animation.length);
        GenericAnimatorPool.Return(animator);
    }
}
