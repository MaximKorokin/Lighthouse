using UnityEngine;

public abstract class HPBarVisualizer : BarAmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        WorldObject = this.GetRequiredComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += (prev, cur, max) => VisualizeAmount(prev, cur, max);
    }
}
