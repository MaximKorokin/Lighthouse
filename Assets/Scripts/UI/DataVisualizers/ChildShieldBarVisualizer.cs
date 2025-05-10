public class ChildShieldBarVisualizer : ShieldBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = GetComponent<WorldCanvasProvider>();
        canvasProvider.CanvasController.HPChildrenSorter.SetChild(BarController.transform, 1);
    }
}
