using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class HPVisualizer : MonoBehaviour
{
    [SerializeField]
    private BarController _barControllerPrefab;

    protected DestroyableWorldObject WorldObject { get; private set; }
    protected BarController BarController { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += VisualizeHPAmount;
    }

    protected virtual void Start()
    {
        BarController = BarsPool.Take(_barControllerPrefab);
        WorldObject.Destroying += () => BarsPool.Return(BarController);
    }

    public virtual void VisualizeHPAmount(float value, float max)
    {
        if (BarController != null)
        {
            BarController.SetFillRatio(value / max);
        }
    }
}
