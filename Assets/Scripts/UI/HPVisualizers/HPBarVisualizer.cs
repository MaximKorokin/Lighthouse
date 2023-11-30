using UnityEngine;

public class HPBarVisualizer : HPVisualizer
{
    [SerializeField]
    private Transform _barParent;

    protected override void Start()
    {
        base.Start();
        BarController.transform.SetParent(_barParent, false);
    }
}
