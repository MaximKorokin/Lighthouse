class DockedHPBarVisualizer : HPVisualizer
{
    private BarController _barController;

    protected override void Awake()
    {
        base.Awake();
        DestroyableWorldObject.Dying += () => HPBarsDock.Return(_barController);
    }

    private void Start()
    {
        _barController = HPBarsDock.Take(DestroyableWorldObject);
    }

    public override void VisualizeHPAmount(float value, float max)
    {
        if (_barController != null)
        {
            _barController.SetFillRatio(value / max);
        }
    }
}