using UnityEngine;

public class LowHPVignetteVisualizer : HPVignetteVisualizer
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _hpThreshold;

    private bool _isShowingOverlay = false;

    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (Overlay == null) return;

        if (cur <= max * _hpThreshold)
        {
            if (!_isShowingOverlay)
            {
                _isShowingOverlay = true;
                Overlay.AnimatorController.PlayAnimation(false);
            }
        }
        else
        {
            if (_isShowingOverlay)
            {
                _isShowingOverlay = false;
                Overlay.AnimatorController.StopAnimation();
            }
        }
    }
}
