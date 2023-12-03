using System;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    [field: SerializeField]
    public PositioningType PositioningType { get; protected set; }
    [field: SerializeField]
    public PositioningType TriggeringType { get; protected set; }

    [SerializeField]
    private Stats _stats;
    protected Stats Stats => _stats;

    public virtual float ActionRange => Stats[StatName.ActionRange] * Stats[StatName.SizeScale];
    public virtual float AttackSpeed => Stats[StatName.AttackSpeed];

    public event Action<AnimatorKey, float> AnimatorValueSet;

    protected virtual void Awake()
    {

    }

    private void OnValidate()
    {
        OnStatsModified();
    }

    protected virtual void Start()
    {
        OnStatsModified();
    }

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
        SetAnimatorValue(AnimatorKey.AttackSpeed, Stats[StatName.AttackSpeed]);
    }

    public void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        AnimatorValueSet?.Invoke(key, Convert.ToSingle(value));
    }
}

[Flags]
public enum PositioningType
{
    None = 0,
    Air = 1,
    Ground = 2,
    Both = Air | Ground
}