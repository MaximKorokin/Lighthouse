using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Must be used in conjunction with instances of <see cref="SingleAnimator"/> class
/// </summary>
[RequireComponent(typeof(WorldObject))]
public class ComplexAnimator : MonoBehaviour, IAnimator
{
    private readonly List<SingleAnimator> _animators = new();
    private WorldObject _worldObject;

    private void Awake()
    {
        _worldObject = GetComponent<WorldObject>();
        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }

    private void Update()
    {
        SetOrdering();
    }

    public void AddAnimator(SingleAnimator animator)
    {
        _animators.Add(animator);
        animator.Initialize();

        _worldObject.VisualSize = GetExtents() * 2;
    }

    public void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        if (_animators.Count == 0)
        {
            return;
        }
        _animators.ForEach(x => x.SetAnimatorValue(key, value));
    }

    public void SetFlip(bool shouldFlip)
    {
        if (_animators.Count == 0)
        {
            return;
        }
        _animators.ForEach(x => x.SetFlip(shouldFlip));
    }

    public Vector2 GetExtents()
    {
        if (_animators.Count == 0)
        {
            return Vector2.zero;
        }
        var allExtents = _animators.Select(x => x.GetExtents()).ToArray();
        var maxX = allExtents.Max(x => x.x);
        var maxY = allExtents.Max(x => x.y);
        return new Vector2(maxX, maxY);
    }

    private void SetOrdering()
    {
        _animators.ForEach(x => x.SetOrdering((Vector2)transform.position));
    }
}
