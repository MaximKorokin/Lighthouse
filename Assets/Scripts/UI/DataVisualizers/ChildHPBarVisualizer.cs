using UnityEngine;

[RequireComponent(typeof(WorldObjectCanvasProvider))]
public class ChildHPBarVisualizer : HPBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldObjectCanvasProvider>();
        BarController.transform.SetParent(canvasProvider.CanvasController.LowerElementsParent, false);
    }
}
