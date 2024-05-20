using UnityEngine;

public class CameraPriorityPhase : ActPhase
{
    [SerializeField]
    private CameraMovementPriority _priorityOverride;

    public override void Invoke()
    {
        MainCameraController.Instance.MinPriority = _priorityOverride;
        InvokeEnded();
    }

    public override string IconName => "CameraPriority.png";
    public override Color IconColor => _priorityOverride < CameraMovementPriority.High ? MyColors.Green : MyColors.Red;
}
