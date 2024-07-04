using UnityEngine;

public class CameraMovePhase : ActPhase
{
    private const CameraMovementPriority Priority = CameraMovementPriority.High;

    [SerializeField]
    private Transform _transformPosition;
    [SerializeField]
    private float _speed;

    public Transform TransformPosition => _transformPosition;

    public override void Invoke()
    {
        if (_transformPosition == null)
        {
            Logger.Warn($"{nameof(_transformPosition)} parameter is not set in {nameof(CameraMovePhase)}");
            return;
        }
        MainCameraController.Instance.SetMovement(_transformPosition.position, _speed, false, Priority, InvokeFinished);
    }

    public override string IconName => "CameraMove.png";
}
