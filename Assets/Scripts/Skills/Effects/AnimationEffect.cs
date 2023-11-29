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
            //animator.transform.SetParent(castState.Target.transform, false);
            animator.transform.parent = castState.Target.transform;
        }
        animator.transform.localPosition = Vector3.zero;
        var animatorSpriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (animatorSpriteRenderer == null)
        {
            return;
        }
        animatorSpriteRenderer.sortingOrder = _orderInLayer;
        if (_flipWithTarget)
        {
            var targetSpriteRenderer = castState.Target.GetComponent<SpriteRenderer>();
            if (targetSpriteRenderer != null)
            {
                animatorSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animatorSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
        }
    }
}
