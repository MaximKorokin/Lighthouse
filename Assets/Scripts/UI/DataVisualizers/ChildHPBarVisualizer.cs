using UnityEngine;

[RequireComponent(typeof(WorldCanvasProvider))]
public class ChildHPBarVisualizer : HPBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldCanvasProvider>();
        canvasProvider.CanvasController.HPChildrenSorter.SetChild(BarController.transform, 11);
    }
}
