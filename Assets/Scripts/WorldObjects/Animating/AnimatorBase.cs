using System;
using UnityEngine;

public abstract class AnimatorBase : MonoBehaviour, IAnimator, IInitializable<AnimatorBase>
{
    [SerializeField]
    private bool _hasExtents = true;
    [SerializeField]
    private float _orderingOffset;
    protected float OrderingOffset { get => _orderingOffset; set => _orderingOffset = value; }

    public event Action<AnimatorBase> Initialized;

    private Animator _animator;
    public Animator Animator => gameObject.LazyGetComponent(ref _animator);

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => gameObject.LazyGetComponent(ref _spriteRenderer);

    public virtual void Initialize()
    {
        Animator.keepAnimatorStateOnDisable = true;
        SetFlip(false);
        Initialized?.Invoke(this);
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
    }

    public Vector2 GetExtents()
    {
        var sprite = SpriteRenderer.sprite;

        if (sprite == null) return Vector2.zero;

        var croppedRect = new Rect(
            (sprite.textureRectOffset.x + sprite.textureRect.width / 2) / sprite.pixelsPerUnit,
            (sprite.textureRectOffset.y + sprite.textureRect.height / 2) / sprite.pixelsPerUnit,
            sprite.textureRect.width / sprite.pixelsPerUnit,
            sprite.textureRect.height / sprite.pixelsPerUnit);

        // In case of using packed sprites use following method
        //var padding = UnityEngine.Sprites.DataUtility.GetPadding(sprite);

        // Visualizing croppedRect
        //var c = gameObject.AddComponent<BoxCollider2D>();
        //c.offset = croppedRect.position - sprite.pivot / sprite.pixelsPerUnit;
        //c.size = croppedRect.size;

        return _hasExtents ? croppedRect.size / 2 : Vector2.zero;
    }

    public void SetOrdering(Vector2 globalPosition)
    {
        var newSortingOrder = -(int)((globalPosition.y + _orderingOffset) * 100);
        SpriteRenderer.sortingOrder = newSortingOrder;
    }
}

public enum AnimatorKey
{
    Attack = 1,
    Hurt = 2,
    IsDead = 3,
    IsMoving = 4,
    HPRatio = 5,
    AttackSpeed = 6,
    MoveSpeed = 7,
    Transit = 8,
    Dash = 9,
    ShieldRatio = 10,
    PlayAnimation = 11,
    StopAnimation = 12,
    HurtShield = 13,
}
