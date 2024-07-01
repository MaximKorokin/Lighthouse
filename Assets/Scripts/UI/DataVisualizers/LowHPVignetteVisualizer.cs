using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class LowHPVignetteVisualizer : HPVignetteVisualizer
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _hpThreshold;

    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (Overlay == null) return;

        if (cur <= max * _hpThreshold)
        {
            Overlay.AnimatorController.PlayAnimation(false);
        }
        else
        {
            Overlay.AnimatorController.StopAnimation();
        }
    }
}
