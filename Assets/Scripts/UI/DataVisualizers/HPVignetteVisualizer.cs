public abstract class HPVignetteVisualizer : VignetteAmountVisualizer
{
    protected DestroyableWorldObject WorldObject { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = this.GetRequiredComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += (prev, cur, max) => VisualizeAmount(prev, cur, max);
    }
}
