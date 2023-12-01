using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class HPVisualizer : AmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        WorldObject = GetComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += VisualizeAmount;
    }

    protected override void Start()
    {
        base.Start();
        WorldObject.Destroying += ReturnBar;
    }
}
