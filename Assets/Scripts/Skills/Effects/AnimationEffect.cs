using System.Collections;
using UnityEngine;

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

    private Coroutine _coroutine;
    private Animator _animator;

    public override void Invoke(CastState castState)
    {
        Cancel(castState.Target);
        _animator = GenericAnimatorPool.Take(_animation);
        SetupAnimator(castState, _animator);
        if (_hasDuration)
        {
            _coroutine = castState.Target.StartCoroutine(AnimationCoroutine(castState.Target));
        }
        if (_childToTarget)
        {
            castState.Target.Destroyed += Cancel;
        }
    }

    private void Cancel(WorldObject worldObject)
    {
        worldObject.Destroyed -= Cancel;
        if (_animator != null)
        {
            GenericAnimatorPool.Return(_animator);
            _animator = null;
        }
        if (_coroutine != null)
        {
            worldObject.StopCoroutine(_coroutine);
        }
    }

    private IEnumerator AnimationCoroutine(WorldObject worldObject)
    {
        yield return new WaitForSeconds(_duration > 0 ? _duration : _animation.length);
        Cancel(worldObject);
    }

    // todo: this is gavno
    private void SetupAnimator(CastState castState, Animator animator)
    {
        if (_childToTarget)
        {
            animator.transform.parent = castState.Target.transform;
            animator.transform.localScale = Vector3.one;
        }
        animator.transform.position = castState.Target.transform.position;

        var animatorSpriteRenderer = animator.GetComponent<SpriteRenderer>();
        animatorSpriteRenderer.sortingOrder = _orderInLayer;

        var targetComplexAnimator = castState.Target.GetComponent<ComplexAnimator>();
        var targetSpriteRenderer = castState.Target.GetComponent<SpriteRenderer>();
        if (targetComplexAnimator != null)
        {
            if (_flipWithTarget && castState.Target is MovableWorldObject movable)
            {
                animatorSpriteRenderer.flipX = movable.IsFlipped;
            }
            if (_positioning == AnimationEffectPositioning.BoundsBottom)
            {
                animator.transform.localPosition = new Vector2(0, -targetComplexAnimator.GetExtents().y);
            }
        }
        else if (targetSpriteRenderer != null)
        {
            if (_flipWithTarget)
            {
                animatorSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animatorSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
            if (_positioning == AnimationEffectPositioning.BoundsBottom)
            {
                animator.transform.localPosition = new Vector2(0, -targetSpriteRenderer.localBounds.extents.y);
            }
        }
    }
}

public enum AnimationEffectPositioning
{
    Center = 0,
    BoundsBottom = 1,
}