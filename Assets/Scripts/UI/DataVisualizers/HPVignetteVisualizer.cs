using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class HPVignetteVisualizer : VignetteAmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += (prev, cur, max) => VisualizeAmount(prev, cur, max);
    }
}
