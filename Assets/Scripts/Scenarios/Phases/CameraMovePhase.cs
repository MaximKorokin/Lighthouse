using UnityEngine;

public class CameraMovePhase : SkippableActPhase
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
        base.Invoke();
        MainCameraController.SetMovement(_transformPosition.position, _speed, false, Priority);
        MainCameraController.MoveFinished += OnMoveFinished;
    }

    private void OnMoveFinished()
    {
        MainCameraController.MoveFinished -= OnMoveFinished;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        MainCameraController.SetMovement(_transformPosition.position, float.PositiveInfinity, false, Priority);
    }

    public override string IconName => "CameraMove.png";
}
