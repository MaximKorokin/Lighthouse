using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class HPVisualizer : MonoBehaviour
{
    protected DestroyableWorldObject DestroyableWorldObject { get; private set; }

    protected virtual void Awake()
    {
        DestroyableWorldObject = GetComponent<DestroyableWorldObject>();
        DestroyableWorldObject.HealthPointsChanged += VisualizeHPAmount;
    }

    public abstract void VisualizeHPAmount(float value, float max);
}
