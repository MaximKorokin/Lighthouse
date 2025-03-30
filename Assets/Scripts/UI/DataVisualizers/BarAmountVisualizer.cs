using System;
using UnityEngine;

public abstract class BarAmountVisualizer : AmountVisualizerBase, IInitializable<BarAmountVisualizer>
{
    [SerializeField]
    private BarController _barControllerPrefab;

    public event Action<BarAmountVisualizer> Initialized;

    public BarController BarController { get; private set; }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_barControllerPrefab == null)
        {
            Logger.Error($"{nameof(_barControllerPrefab)} is null in {name}");
            return;
        }

        BarController = BarsPool.Take(_barControllerPrefab);
        VisualizeAmount(1, 1, 1);
        Initialized?.Invoke(this);
    }

    protected virtual void OnDestroy()
    {
        ReturnBar();
    }

    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (BarController != null)
        {
            BarController.SetFillRatio(max > 0 ? (cur / max) : 0);
        }
    }

    protected void ReturnBar()
    {
        if (BarController == null)
        {
            return;
        }
        BarsPool.Return(BarController);
        BarController = null;
    }
}
