using System;
using UnityEngine;

/// <summary>
/// Must be used in conjunction with <see cref="ComplexAnimator"/> which finds instances of this class
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SingleAnimator : MonoBehaviour, IAnimator
{
    [SerializeField]
    private bool _hasExtents = true;
    [SerializeField]
    private Vector2 _offset;
    private Vector2 _shift;
    private Vector3 _initialLocalPosition;

    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public virtual void Initialize()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _initialLocalPosition = transform.localPosition;
        SetFlip(false);
    }

    public void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        if (Animator == null || Animator.runtimeAnimatorController == null)
        {
            return;
        }
        var keyName = key.ToString();

        switch (Array.Find(Animator.parameters, x => x.name == keyName)?.type)
        {
            case AnimatorControllerParameterType.Bool:
                Animator.SetBool(keyName, Convert.ToBoolean(value));
                break;
            case AnimatorControllerParameterType.Trigger:
                Animator.SetTrigger(keyName);
                break;
            case AnimatorControllerParameterType.Int:
                Animator.SetInteger(keyName, Convert.ToInt32(value));
                break;
            case AnimatorControllerParameterType.Float:
                Animator.SetFloat(keyName, Convert.ToSingle(value));
                break;
        }
    }

    public void SetFlip(bool shouldFlip)
    {
        SpriteRenderer.flipX = shouldFlip;
        Reposition();
    }

    private void Reposition()
    {
        transform.localPosition = _initialLocalPosition;
        transform.localPosition += new Vector3((SpriteRenderer.flipX ? -1 : 1) * _offset.x, _offset.y) + (Vector3)_shift;
    }

    public Vector2 GetExtents()
    {
        return _hasExtents ? ((Vector2)SpriteRenderer.localBounds.extents) - _offset - _shift : Vector2.zero;
    }

    public void SetShift(Vector2 shift)
    {
        _shift = shift;
        Reposition();
    }
}

public enum AnimatorKey
{
    Attack = 1,
    Hurt = 2,
    Dead = 3,
    IsMoving = 4,
    HPRatio = 5,
    AttackSpeed = 6,
    MoveSpeed = 7,
    Transit = 8,
    Dash = 9,
}
