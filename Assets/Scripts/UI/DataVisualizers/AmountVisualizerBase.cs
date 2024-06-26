using UnityEngine;

public abstract class AmountVisualizerBase : MonoBehaviour
{
    public abstract void VisualizeAmount(float prev, float cur, float max);
}
