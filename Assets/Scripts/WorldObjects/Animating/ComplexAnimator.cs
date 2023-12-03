using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class ComplexAnimator : MonoBehaviour, IAnimator
{
    private SingleAnimator[] _animators;

    private void Awake()
    {
        var _worldObject = GetComponent<WorldObject>();
        _animators = GetComponentsInChildren<SingleAnimator>();
        var animator = GetComponent<SingleAnimator>();
        if (animator != null)
        {
            _animators = _animators.Concat(animator.Yield()).ToArray();
        }

        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }

    public void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        if (_animators.Length == 0)
        {
            return;
        }
        _animators.ForEach(x => x.SetAnimatorValue(key, value));
    }

    public void SetFlip(bool shouldFlip)
    {
        if (_animators.Length == 0)
        {
            return;
        }
        _animators.ForEach(x => x.SetFlip(shouldFlip));
    }

    public Vector2 GetExtents()
    {
        if (_animators.Length == 0)
        {
            return Vector2.zero;
        }
        var allExtents = _animators.Select(x => x.GetExtents()).ToArray();
        var maxX = allExtents.Max(x => x.x);
        var maxY = allExtents.Max(x => x.y);
        return new Vector2(maxX, maxY);
    }
}
