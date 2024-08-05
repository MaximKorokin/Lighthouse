using UnityEngine;

/// <summary>
/// It is <see cref="SingleAnimator"/> and <see cref="ComplexAnimator"/> classes in one
/// </summary>
[RequireComponent(typeof(WorldObject))]
public class SimpleAnimator : SingleAnimator
{
    private WorldObject _worldObject;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        SetOrdering((Vector2)transform.position + _worldObject.VisualPositionOffset);
    }

    public override void Initialize()
    {
        base.Initialize();
        _worldObject = GetComponent<WorldObject>();
        _worldObject.AnimatorValueSet += SetAnimatorValue;
        if (_worldObject is MovableWorldObject movable)
        {
            movable.Flipped += SetFlip;
        }
    }
}
