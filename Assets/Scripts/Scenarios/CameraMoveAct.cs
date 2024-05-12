using UnityEngine;

public class CameraMoveAct : ScenarioAct
{
    [SerializeField]
    private Transform _transformPosition;
    [SerializeField]
    private Vector3 _position;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private CameraMovementPriority _priorityOverride;

    protected override void Act()
    {
        var position = _transformPosition == null ? _position : _transformPosition.position;
        MainCameraController.Instance.MinPriority = _priorityOverride;
        MainCameraController.Instance.SetMovement(position, _speed, false, _priorityOverride, () => IsUsed = true);
    }
}
