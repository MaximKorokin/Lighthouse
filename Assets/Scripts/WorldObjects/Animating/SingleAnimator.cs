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
    private Vector2 _offset;
    private Vector3 _initialLocalPosition;

    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    protected virtual void Awake()
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
        transform.localPosition = _initialLocalPosition;
        transform.localPosition += new Vector3((shouldFlip ? -1 : 1) * _offset.x, _offset.y);
    }

    public Vector2 GetExtents()
    {
        return ((Vector2)SpriteRenderer.localBounds.extents) - _offset;
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
