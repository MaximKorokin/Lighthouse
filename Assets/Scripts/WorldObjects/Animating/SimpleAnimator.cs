using UnityEngine;

/// <summary>
/// It is <see cref="SingleAnimator"/> and <see cref="ComplexAnimator"/> classes in one
/// </summary>
[RequireComponent(typeof(WorldObject))]
public class SimpleAnimator : SingleAnimator
{
    protected override void Awake()
    {
        base.Awake();
        var _worldObject = GetComponent<WorldObject>();
        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }
}
