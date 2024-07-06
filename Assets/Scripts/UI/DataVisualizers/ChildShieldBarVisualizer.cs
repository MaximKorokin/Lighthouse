using UnityEngine;

[RequireComponent(typeof(WorldObjectCanvasProvider))]
public class ChildShieldBarVisualizer : ShieldBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldObjectCanvasProvider>();
        BarController.transform.SetParent(canvasProvider.CanvasController.LowerElementsParent, false);
    }
}
