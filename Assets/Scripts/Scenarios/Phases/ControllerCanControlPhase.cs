using UnityEngine;

public class ControllerCanControlPhase : ActPhase
{
    [SerializeField]
    private ControllerBase _controller;
    [SerializeField]
    private bool _canControl;

    public override void Invoke()
    {
        _controller.CanControl = _canControl;
    }

    public override string IconName => base.IconName;
}
