using System;
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

    public override void Invoke(CastState castState)
    {
        var animator = GenericAnimatorPool.Take(_animation);
        var target = castState.GetTarget();

        if (_childToTarget)
        {
            animator.transform.parent = target.transform;
            animator.transform.localScale = Vector3.one;

            target.Destroyed += OnWorldObjectDestroyed;
        }

        if (_hasDuration)
        {
            target.StartCoroutine(AnimationCoroutine(animator, target, OnWorldObjectDestroyed));
        }

        SetupAnimator(animator, target, castState.GetTargetPosition());

        void OnWorldObjectDestroyed(WorldObject worldObject)
        {
            Cancel(animator, worldObject, OnWorldObjectDestroyed);
            worldObject.Destroyed -= OnWorldObjectDestroyed;
        }
    }

    private IEnumerator AnimationCoroutine(Animator animator, WorldObject worldObject, Action<WorldObject> onDestroyedAction)
    {
        yield return new WaitForSeconds(_duration > 0 ? _duration : _animation.length);
        Cancel(animator, worldObject, onDestroyedAction);
    }

    private void Cancel(Animator animator, WorldObject worldObject, Action<WorldObject> onDestroyedAction)
    {
        worldObject.Destroyed -= onDestroyedAction;
        if (animator != null)
        {
            GenericAnimatorPool.Return(animator);
        }
    }

    // this is gavno
    private void SetupAnimator(Animator animator, WorldObject target, Vector2 position)
    {
        animator.transform.position = position;

        var animatorSpriteRenderer = animator.GetComponent<SpriteRenderer>();
        animatorSpriteRenderer.sortingOrder = _orderInLayer;

        var targetComplexAnimator = target.GetComponent<ComplexAnimator>();
        var targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        if (targetComplexAnimator != null)
        {
            if (_flipWithTarget && target is MovableWorldObject movable)
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