class DockedHPBarVisualizer : HPVisualizer
{
    private BarController _barController;

    private void Start()
    {
        _barController = HPBarsDock.Take(DestroyableWorldObject);
        DestroyableWorldObject.Destroying += () => HPBarsDock.Return(_barController);
        VisualizeHPAmount(DestroyableWorldObject.CurrentHealthPoints, DestroyableWorldObject.MaxHealthPoints);
    }

    public override void VisualizeHPAmount(float value, float max)
    {
        if (_barController != null)
        {
            _barController.SetFillRatio(value / max);
        }
    }
}