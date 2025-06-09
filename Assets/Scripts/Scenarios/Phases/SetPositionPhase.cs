using UnityEngine;

public class SetPositionPhase : ActPhase
{
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private Transform _transformPosition;

    public Transform TargetTransform => _targetTransform;
    public Transform TransformPosition => _transformPosition;

    public override void Invoke()
    {
        _targetTransform.position = _transformPosition.position;
        InvokeFinished();
    }

    public override string IconName => "T.png";
}
