using UnityEngine;

public class ControlInputSwitchPhase : ActPhase
{
    [SerializeField]
    private bool _turnOn;

    public override void Invoke()
    {
        InputReader.IsControlInputBlocked = !_turnOn;
        InvokeFinished();
    }

    public override string IconName => "Gamepad.png";
    public override Color IconColor => _turnOn ? MyColors.Green : MyColors.Red;
}
