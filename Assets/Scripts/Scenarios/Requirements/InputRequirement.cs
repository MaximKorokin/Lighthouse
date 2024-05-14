public class InputRequirement : ActRequirement
{
    private bool _anyKeyClicked;

    private void Awake()
    {
        _anyKeyClicked = false;
        InputManager.AnyKeyClicked -= OnAnyKeyClicked;
        InputManager.AnyKeyClicked += OnAnyKeyClicked;
    }

    private void OnAnyKeyClicked()
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
        InputManager.AnyKeyClicked -= OnAnyKeyClicked;
    }

    public override string IconName => "Input";
}
