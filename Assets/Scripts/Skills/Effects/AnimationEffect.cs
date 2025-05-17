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
    private float _orderInLayer;
    [SerializeField]
    private AnimationPositioning _positioning;
    [SerializeField]
    private SortingLayer _sortingLayer = SortingLayer.Effects;

    public override void Invoke(CastState castState)
    {
        var animator = GenericAnimatorPool.Take(_animation);
        animator.PlayAnimation(false);
        var target = castState.GetTarget();

        var genericSimpleAnimator = animator.GetComponent<GenericSimpleAnimator>();
        genericSimpleAnimator.SetLayerAndOrderingOffset(_sortingLayer.ToString(), _orderInLayer);

        if (_childToTarget)
        {
            animator.transform.parent = target.transform;
            animator.transform.localScale = Vector3.one;
            animator.transform.localEulerAngles = Vector3.zero;

            if (!_hasDuration)
            {
                target.OnDestroyed(() => Cancel(animator, target, genericSimpleAnimator));
            }
        }

        if (_hasDuration)
        {
            target.StartCoroutineSafe(CoroutinesUtils.WaitForSeconds(_duration > 0 ? _duration : _animation.length), () => Cancel(animator, target, genericSimpleAnimator));
        }

        SetupAnimator(animator, genericSimpleAnimator, target, castState.GetTargetPosition());
    }

    private void SetupAnimator(GenericAnimatorController animator, GenericSimpleAnimator genericSimpleAnimator, WorldObject target, Vector2 position)
    {
        if (_positioning == AnimationPositioning.HalfTargetVisualHeight)
            position += target.VisualSize * Vector2.up * 0.5f;

        animator.transform.position = position;

        if (_flipWithTarget)
        {
            if (target is MovableWorldObject movable)
            {
                movable.Flipped += genericSimpleAnimator.SetFlip;
                genericSimpleAnimator.SetFlip(movable.IsFlipped);
            }
            else if (animator.TryGetComponent<SpriteRenderer>(out var animatorSpriteRenderer) && target.TryGetComponent<SpriteRenderer>(out var targetSpriteRenderer))
            {
                animatorSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animatorSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
        }
    }

    private static void Cancel(GenericAnimatorController animator, WorldObject worldObject, GenericSimpleAnimator genericSimpleAnimator)
    {
        if (worldObject is MovableWorldObject movable && genericSimpleAnimator != null)
        {
            movable.Flipped -= genericSimpleAnimator.SetFlip;
        }
        if (animator != null)
        {
            GenericAnimatorPool.Return(animator);
        }
    }
}

public enum AnimationPositioning
{
    TargetPosition = 0,
    HalfTargetVisualHeight = 10,
}

public enum SortingLayer
{
    Effects = 0,
    WorldObjects = 10,
    Underlay = 100,
}
