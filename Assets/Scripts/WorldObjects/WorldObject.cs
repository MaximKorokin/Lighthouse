using System;
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

    public Vector2 VisualPositionOffset { get; set; }

    [SerializeField]
    private Stats _stats;
    public Stats Stats => _stats;

    public virtual float ActionRange => Stats[StatName.ActionRange] * Stats[StatName.SizeScale];
    public virtual float VisionRange => Stats[StatName.VisionRange] * Stats[StatName.SizeScale];
    public virtual float AttackSpeed => Stats[StatName.ActionCDModifier];

    public event Action<AnimatorKey, float> AnimatorValueSet;
    public event Action Destroyed;

    protected Collider2D[] Colliders;

    protected virtual void Awake()
    {
        Stats.Modified += OnStatsModified;
        Colliders = GetComponents<Collider2D>();
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
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != transform.localScale.z)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
        Colliders?.Select(x => x as CircleCollider2D).Where(x => x != null && x.isTrigger).ForEach(x => x.radius = VisionRange);
        SetAnimatorValue(AnimatorKey.AttackSpeed, Stats[StatName.ActionCDModifier]);
    }

    public virtual void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct
    {
        AnimatorValueSet?.Invoke(key, Convert.ToSingle(value));
    }

    public void SetFaction(Faction faction)
    {
        Faction = faction;
        // Needs this action to retrigger colliders and triggers with a new faction
        ReloadPhysicsState();
    }

    public void ReloadPhysicsState()
    {
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
