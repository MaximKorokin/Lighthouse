using System;
using UnityEngine;

public abstract class BarAmountVisualizer : MonoBehaviour, IInitializable<BarAmountVisualizer>
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
        BarController = BarsPool.Take(_barControllerPrefab);
        VisualizeAmount(1, 1);
        Initialized?.Invoke(this);
    }

    protected virtual void OnDestroy()
    {
        ReturnBar();
    }

    public virtual void VisualizeAmount(float value, float max)
    {
        if (BarController != null)
        {
            BarController.SetFillRatio(value / max);
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