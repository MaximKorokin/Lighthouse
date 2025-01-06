public class PermissionRequirement : ActRequirement
{
    private BoolCounter _hasPermission = new(false);

    private void Awake()
    {
        _hasPermission.ValueChanged += v =>
        {
            if (v) InvokeFulfilled();
        };
    }

    public override bool IsFulfilled()
    {
        return _hasPermission;
    }

    public void Permit()
    {
        _hasPermission.Set(true);
    }

    public void Forbid()
    {
        _hasPermission.Set(false);
    }

    public override string IconName => "Tick.png";
}
