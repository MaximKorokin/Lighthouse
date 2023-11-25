using UnityEngine;

public class HPBarVisualizer : HPVisualizer
{
    [SerializeField]
    private BarController _barController;

    public override void VisualizeHPAmount(float value, float max)
    {
        if (_barController != null)
        {
            _barController.SetFillRatio(value / max);
        }
    }
}
