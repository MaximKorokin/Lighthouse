using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class HPBarVisualizer : BarAmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        WorldObject = GetComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += (prev, cur, max) => VisualizeAmount(cur, max);
    }
}
