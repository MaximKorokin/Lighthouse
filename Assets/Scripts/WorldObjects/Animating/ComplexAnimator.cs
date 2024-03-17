using System.Linq;
using UnityEngine;

/// <summary>
/// Must be used in conjunction with instances of <see cref="SingleAnimator"/> class
/// </summary>
[RequireComponent(typeof(WorldObject))]
public class ComplexAnimator : MonoBehaviour, IAnimator
{
    [SerializeField]
    private InitialPositionShift _shift;

    private SingleAnimator[] _animators;
    private WorldObject _worldObject;

    private void Awake()
    {
        _worldObject = GetComponent<WorldObject>();
        _animators = GetComponentsInChildren<SingleAnimator>();
        if (TryGetComponent<SingleAnimator>(out var animator))
        {
            _animators = _animators.Concat(animator.Yield()).ToArray();
        }

        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }

        if (_shift == InitialPositionShift.HalfUp)
        {
            SetShift(new Vector2(0, GetExtents().y));
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

    public void SetShift(Vector2 shift)
    {
        _animators.ForEach(x => x.SetShift(shift));
        _worldObject.VisualPositionOffset = shift;
    }
}

public enum InitialPositionShift
{
    None = 0,
    HalfUp = 1,
}
