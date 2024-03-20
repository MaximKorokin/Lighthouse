using UnityEngine;

[RequireComponent(typeof(BarAmountVisualizer))]
public class ChildShieldBarVisualizer : ShieldBarVisualizer
{
    private BarAmountVisualizer _parentBarVisualizer;
    private bool _isParentInitialized;
    private bool _isInitialized;

    protected override void Awake()
    {
        base.Awake();
        _parentBarVisualizer = GetComponent<BarAmountVisualizer>();
        _parentBarVisualizer.Initialized += OnParentBarInitialized;
        Initialized += OnInitialized;
    }

    private void OnInitialized(BarAmountVisualizer obj)
    {
        Initialized -= OnInitialized;
        _isInitialized = true;
        if (_isParentInitialized)
        {
            BarController.transform.SetParent(_parentBarVisualizer.BarController.transform, false);
        }
    }

    private void OnParentBarInitialized(BarAmountVisualizer initializable)
    {
        _parentBarVisualizer.Initialized -= OnParentBarInitialized;
        _isParentInitialized = true;
        if (_isInitialized)
        {
            BarController.transform.SetParent(_parentBarVisualizer.BarController.transform, false);
        }
    }
}
