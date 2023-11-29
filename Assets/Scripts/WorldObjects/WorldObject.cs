using System;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    [field: SerializeField]
    public PositioningType PositioningType { get; protected set; }
    [field: SerializeField]
    public PositioningType TriggeringType { get; protected set; }
    private Animator Animator { get; set; }

    [SerializeField]
    private Stats _stats;
    protected Stats Stats => _stats;

    public virtual float ActionRange => Stats[StatName.ActionRange] * Stats[StatName.SizeScale];

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        OnStatsModified();
    }

    private void OnValidate()
    {
        OnStatsModified();
    }

    protected virtual void Start() { }

    public void ModifyStats(Stats otherStats)
    {
        Stats.Modify(otherStats);
        OnStatsModified();
    }

    /// <summary>
    /// Called on initialization and each time <see cref="ModifyStats"/> is called
    /// </summary>
    protected virtual void OnStatsModified()
    {
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != transform.localScale.z)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
    }

    public void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        if (Animator == null)
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
}

[Flags]
public enum PositioningType
{
    None = 0,
    Flying = 1,
    Walking = 2,
    Both = Flying | Walking
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
}