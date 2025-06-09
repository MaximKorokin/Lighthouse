using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    [field: SerializeField]
    public PositioningType PositioningType { get; set; }
    [field: SerializeField]
    public PositioningType TriggeringType { get; set; }
    [field: SerializeField]
    public Faction Faction { get; private set; }

    public Vector2 VisualSize { get; set; }

    [SerializeField]
    private Stats _stats;
    public Stats Stats => _stats;

    public virtual float ActionRange => Stats[StatName.ActionRange] * Stats[StatName.SizeScale];
    public virtual float VisionRange => Stats[StatName.VisionRange] * Stats[StatName.SizeScale];
    public virtual float AttackSpeed => Stats[StatName.ActionCDModifier];

    public event Action<AnimatorKey, float> AnimatorValueSet;
    public event Action Destroyed;
    public event Action PhysicsStateReloading;

    private Collider2D[] _colliders;
    public IEnumerable<Collider2D> Colliders => _colliders;
    public Collider2D MainCollider { get; private set; }

    protected virtual void Awake()
    {
        Stats.Modified += OnStatsModified;
        _colliders = GetComponents<Collider2D>();
        MainCollider = _colliders.FirstOrDefault(x => x.includeLayers == 0);
    }

    private void OnValidate()
    {
        OnStatsModified();
    }

    protected virtual void Start()
    {
        OnStatsModified();
    }

    protected virtual void OnDestroy()
    {
        StopAllCoroutines();
        Destroyed?.Invoke();
    }

    /// <summary>
    /// Called on initialization and each time <see cref="Stats.Modify"/> is called
    /// </summary>
    protected virtual void OnStatsModified()
    {
        if (Stats == null)
        {
            return;
        }
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != transform.localScale.z)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
        SetAnimatorValue(AnimatorKey.AttackSpeed, Stats[StatName.ActionCDModifier]);
    }

    public virtual void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        AnimatorValueSet?.Invoke(key, Convert.ToSingle(value));
    }

    public void SetFaction(Faction faction)
    {
        Faction = faction;
        ReloadPhysicsState();
    }

    // Needs this action to retrigger colliders and triggers with a new faction
    public void ReloadPhysicsState()
    {
        PhysicsStateReloading?.Invoke();
        foreach (var item in Colliders)
        {
            item.enabled = false;
            item.enabled = true;
        }
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
