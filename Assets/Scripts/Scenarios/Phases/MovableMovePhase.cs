using System.Collections;
using UnityEngine;

public class MovableMovePhase : SkippableActPhase
{
    [SerializeField]
    private MovableWorldObject _movable;
    [SerializeField]
    private bool _overrideController = true;
    [SerializeField]
    private Transform _transformPosition;

    public MovableWorldObject Movable => _movable;
    public Transform TransformPosition => _transformPosition;

    public override void Invoke()
    {
        if (_transformPosition == null)
        {
            Logger.Warn($"{nameof(_transformPosition)} parameter is not set in {nameof(MovableMovePhase)}");
            return;
        }
        base.Invoke();
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        if (_movable.TryGetComponent<ControllerBase>(out var controller) && _overrideController)
        {
            controller.CanControl = false;
        }

        while (((Vector2)(_transformPosition.position - _movable.transform.position)).sqrMagnitude > 0.01f)
        {
            _movable.Direction = ((Vector2)(_transformPosition.position - _movable.transform.position)).normalized;
            _movable.Move();

            yield return new WaitForEndOfFrame();
        }

        _movable.Stop();

        if (controller != null)
        {
            controller.CanControl = true;
        }

        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _movable.transform.position = _transformPosition.position;
    }

    public override string IconName => "WOMove.png";
}
