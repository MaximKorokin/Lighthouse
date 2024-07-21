using UnityEngine;

[RequireComponent(typeof(WorldCanvasProvider))]
public class ChildShieldBarVisualizer : ShieldBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldCanvasProvider>();
        BarController.transform.SetParent(canvasProvider.CanvasController.HPViewParent, false);
    }
}
