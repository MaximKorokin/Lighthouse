using UnityEngine;

[RequireComponent(typeof(WorldCanvasProvider))]
public class ChildHPBarVisualizer : HPBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldCanvasProvider>();
        BarController.transform.SetParent(canvasProvider.CanvasController.HPViewParent, false);
    }
}
