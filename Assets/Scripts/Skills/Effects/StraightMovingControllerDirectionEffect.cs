using UnityEngine;

public class StraightMovingControllerDirectionEffect : Effect
{
    [SerializeField]
    private Vector2 _initialDirection;
    [SerializeField]
    private DirectionType _directionType;

    public override void Invoke(CastState castState)
    {
        if (!castState.InitialSource.TryGetComponent<StraightMovingController>(out var controller))
        {
            Logger.Warn($"Target does not have {nameof(StraightMovingController)} in {nameof(StraightMovingControllerDirectionEffect)}");
            return;
        }

        if (_directionType == DirectionType.None)
        {
            controller.Direction = Vector2.zero;
            return;
        }

        var direction = (_initialDirection + (Vector2)castState.GetTarget().transform.position - (Vector2)controller.transform.position).normalized;
        if (_directionType == DirectionType.ToTarget)
        {
            controller.Direction = direction;
        }
        else if (_directionType == DirectionType.FromTarget)
        {
            controller.Direction = direction * -1;
        }
    }

    private enum DirectionType
    {
        None = 0,
        ToTarget = 1,
        FromTarget = 2,
    }
}

