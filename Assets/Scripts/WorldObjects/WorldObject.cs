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

    protected virtual void Awake()
    {
        Stats.Init();
        OnStatsModified();
    }

    public void ModifyStats(Stats otherStats)
    {
        Stats.Modify(otherStats);
        OnStatsModified();
    }

    /// <summary>
    /// Called on Awake and each time <see cref="ModifyStats"/> is called
    /// </summary>
    protected virtual void OnStatsModified()
    {
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != transform.localScale.z)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
    }
}
