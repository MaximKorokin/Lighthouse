using UnityEngine;

public class PermissionPhase : ActPhase
{
    [SerializeField]
    private PermissionRequirement _permissionRequirement;
    [SerializeField]
    private bool _permit;

    public PermissionRequirement PermissionRequirement => _permissionRequirement;

    public override void Invoke()
    {
        if (_permit)
        {
            _permissionRequirement.Permit();
        }
        else
        {
            _permissionRequirement.Forbid();
        }
        InvokeFinished();
    }

    public override string IconName => "Tick.png";
    public override Color IconColor => _permit ? MyColors.Green : MyColors.Red;
}
