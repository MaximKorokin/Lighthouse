class DockedHPBarVisualizer : HPVisualizer
{
    protected override void Start()
    {
        base.Start();
        BarController.transform.SetParent(HPBarsDock.Instance.transform, false);
    }
}