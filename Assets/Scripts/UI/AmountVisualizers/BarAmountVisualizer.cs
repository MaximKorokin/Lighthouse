using UnityEngine;

public abstract class BarAmountVisualizer : MonoBehaviour
{
    [SerializeField]
    private BarController _barControllerPrefab;
    protected BarController BarController { get; private set; }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        BarController = BarsPool.Take(_barControllerPrefab);
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