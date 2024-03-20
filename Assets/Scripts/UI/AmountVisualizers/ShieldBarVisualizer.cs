using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class ShieldBarVisualizer : BarAmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        WorldObject = GetComponent<DestroyableWorldObject>();
        WorldObject.ShieldValueChanged += (prev, cur, max) => VisualizeAmount(cur, max);
    }
}
