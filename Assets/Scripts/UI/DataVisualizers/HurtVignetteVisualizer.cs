using UnityEngine;

public class HurtVignetteVisualizer : HPVignetteVisualizer
{
    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (prev > cur)
        {
            Overlay.AnimatorController.PlayAnimation(true);
        }
    }
}
