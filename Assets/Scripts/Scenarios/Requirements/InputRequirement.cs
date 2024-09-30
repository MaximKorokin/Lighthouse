public class InputRequirement : ActRequirement
{
    private bool _anyKeyClicked;

    private void Awake()
    {
        _anyKeyClicked = false;
        InputReader.AnyKeyClicked += OnAnyKeyClicked;
    }

    private void OnAnyKeyClicked(bool _)
    {
        _anyKeyClicked = true;
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return _anyKeyClicked;
    }

    private void OnDestroy()
    {
        InputReader.AnyKeyClicked -= OnAnyKeyClicked;
    }

    public override string IconName => "Input";
}
