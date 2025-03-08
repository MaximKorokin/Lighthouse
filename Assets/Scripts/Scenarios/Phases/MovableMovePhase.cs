using System.Collections;
using UnityEngine;

public class MovableMovePhase : SkippableActPhase
{
    [SerializeField]
    private MovableWorldObject _movable;
    [SerializeField]
    private bool _overrideController = true;
    [SerializeField]
    private Transform[] _transformPositions;

    private int _transformPositionsIndex;

    public MovableWorldObject Movable => _movable;
    public Transform[] TransformPositions => _transformPositions;

    public override void Invoke()
    {
        if (_transformPositions == null || _transformPositions.Length == 0)
        {
            Logger.Warn($"{nameof(_transformPositions)} parameter is not set in {nameof(MovableMovePhase)}");
            return;
        }
        base.Invoke();
        _transformPositionsIndex = 0;
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        if (_movable.TryGetComponent<ControllerBase>(out var controller) && _overrideController)
        {
            controller.CanControl = false;
        }

        for (_transformPositionsIndex = 0; _transformPositionsIndex < _transformPositions.Length; _transformPositionsIndex++)
        {
            while (((Vector2)(_transformPositions[_transformPositionsIndex].position - _movable.transform.position)).sqrMagnitude > 0.01f)
            {
                _movable.Direction = ((Vector2)(_transformPositions[_transformPositionsIndex].position - _movable.transform.position)).normalized;
                _movable.Move();

                yield return new WaitForEndOfFrame();
            }
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
        _movable.transform.position = _transformPositions[_transformPositionsIndex].position;
    }

    public override string IconName => "WOMove.png";
}
