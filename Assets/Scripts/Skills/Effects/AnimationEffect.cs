using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "ScriptableObjects/Effects/AnimationEffect", order = 1)]
public class AnimationEffect : Effect
{
    [SerializeField]
    private AnimationClip _animation;
    [SerializeField]
    private bool _hasDuration;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private bool _childToTarget;
    [SerializeField]
    private bool _flipWithTarget;
    [SerializeField]
    private int _orderInLayer;
    [SerializeField]
    private AnimationEffectPositioning _positioning;

    public override void Invoke(CastState castState)
    {
        Coroutine coroutine = null;
        var animator = GenericAnimatorPool.Take(_animation);
        if (_hasDuration)
        {
            coroutine = castState.Target.StartCoroutine(AnimationCoroutine(castState, animator));
        }
        else
        {
            SetupAnimator(castState, animator);
        }
        if (castState.Target is DestroyableWorldObject destroyable)
        {
            destroyable.Destroying += () =>
            {
                GenericAnimatorPool.Return(animator);
                if (coroutine != null)
                {
                    castState.Target.StopCoroutine(coroutine);
                }
            };
        }
    }

    private IEnumerator AnimationCoroutine(CastState castState, Animator animator)
    {
        SetupAnimator(castState, animator);
        yield return new WaitForSeconds(_duration > 0 ? _duration : _animation.length);
        GenericAnimatorPool.Return(animator);
    }

    private void SetupAnimator(CastState castState, Animator animator)
    {
        if (_childToTarget)
        {
            animator.transform.parent = castState.Target.transform;
            animator.transform.localScale = Vector3.one;
        }
        animator.transform.localPosition = Vector3.zero;

        var animatorSpriteRenderer = animator.GetComponent<SpriteRenderer>();
        animatorSpriteRenderer.sortingOrder = _orderInLayer;

        var targetSpriteRenderer = castState.Target.GetComponent<SpriteRenderer>();
        if (targetSpriteRenderer != null)
        {
            if (_flipWithTarget)
            {
                animatorSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animatorSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
            if (_positioning == AnimationEffectPositioning.SpriteBottom)
            {
                animator.transform.localPosition = new Vector2(0, -targetSpriteRenderer.localBounds.extents.y);
            }
        }
    }
}

public enum AnimationEffectPositioning
{
    Center = 0,
    SpriteBottom = 1,
}