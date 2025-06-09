using UnityEngine;

/// <summary>
/// It is <see cref="SingleAnimator"/> and <see cref="ComplexAnimator"/> classes in one
/// </summary>
public class SimpleAnimator : AnimatorBase
{
    private WorldObject _worldObject;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        SetOrdering((Vector2)transform.position);
    }

    public override void Initialize()
    {
        base.Initialize();
        _worldObject = this.GetRequiredComponent<WorldObject>();
        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }
}
