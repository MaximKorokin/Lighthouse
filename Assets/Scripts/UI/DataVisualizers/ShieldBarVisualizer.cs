public abstract class ShieldBarVisualizer : BarAmountVisualizer
{
    protected DestroyableShield DestroyableShield { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DestroyableShield = GetComponent<DestroyableShield>();
        DestroyableShield.ShieldValueChanged += (prev, cur, max) => VisualizeAmount(prev, cur, max);
    }
}
