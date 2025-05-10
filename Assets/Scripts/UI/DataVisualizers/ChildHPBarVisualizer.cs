public class ChildHPBarVisualizer : HPBarVisualizer
{
    protected override void Start()
    {
        base.Start();
        var canvasProvider = this.GetRequiredComponent<WorldCanvasProvider>();
        canvasProvider.CanvasController.HPChildrenSorter.SetChild(BarController.transform, 11);
    }
}
