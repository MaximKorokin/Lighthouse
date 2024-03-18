using UnityEngine;

/// <summary>
/// It is <see cref="SingleAnimator"/> and <see cref="ComplexAnimator"/> classes in one
/// </summary>
[RequireComponent(typeof(WorldObject))]
public class SimpleAnimator : SingleAnimator
{
    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        var _worldObject = GetComponent<WorldObject>();
        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }
}
