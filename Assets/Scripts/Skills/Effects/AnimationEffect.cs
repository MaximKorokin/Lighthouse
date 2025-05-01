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
    private AnimationPositioning _positioning;
    [SerializeField]
    private SortingLayer _sortingLayer = SortingLayer.Effects;

    public override void Invoke(CastState castState)
    {
        var animator = GenericAnimatorPool.Take(_animation);
        animator.PlayAnimation(false);
        var target = castState.GetTarget();

        if (_childToTarget)
        {
            animator.transform.parent = target.transform;
            animator.transform.localScale = Vector3.one;
            animator.transform.localEulerAngles = Vector3.zero;

            if (!_hasDuration)
            {
                target.OnDestroyed(() => Cancel(animator));
            }
        }

        if (_hasDuration)
        {
            target.StartCoroutineSafe(CoroutinesUtils.WaitForSeconds(_duration > 0 ? _duration : _animation.length), () => Cancel(animator));
        }

        SetupAnimator(animator, target, castState.GetTargetPosition());
    }

    private void SetupAnimator(GenericAnimatorController animator, WorldObject target, Vector2 position)
    {
        if (_positioning == AnimationPositioning.HalfTargetVisualHeight)
            position += target.VisualSize * Vector2.up * 0.5f;

        animator.transform.position = position;

        var animatorSpriteRenderer = animator.GetComponent<SpriteRenderer>();
        animatorSpriteRenderer.sortingLayerName = _sortingLayer.ToString();
        animatorSpriteRenderer.sortingOrder = _orderInLayer;

        var targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        if (_flipWithTarget)
        {
            if (target is MovableWorldObject movable)
            {
                animatorSpriteRenderer.flipX = movable.IsFlipped;
            }
            else if (targetSpriteRenderer != null)
            {
                animatorSpriteRenderer.flipX = targetSpriteRenderer.flipX;
                animatorSpriteRenderer.flipY = targetSpriteRenderer.flipY;
            }
        }
    }

    private static void Cancel(GenericAnimatorController animator)
    {
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
}
