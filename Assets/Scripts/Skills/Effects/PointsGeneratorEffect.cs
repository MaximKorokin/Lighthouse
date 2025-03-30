using UnityEngine;

public class PointsGeneratorEffect : ComplexEffect
{
    [SerializeField]
    private PointsGeneratingData _data;

    public override void Invoke(CastState castState)
    {
        castState.TargetingType = TargetingType.Point;

        var initialPosition = GetFulcrumPosition(castState);
        var direction = GetDirection(castState);

        for (int i = 0; i < _data.Number; i++)
        {
            var point = initialPosition + direction.Rotate(GetAngle(i)) * GetDistance();
            castState.Payload = new PointCastStatePayload(point);
            base.Invoke(castState);
        }
    }

    private Vector2 GetFulcrumPosition(CastState castState)
    {
        return _data.GeneratingFulcrum switch
        {
            PointsGeneratingFulcrum.Target => castState.Target.transform.position,
            PointsGeneratingFulcrum.Source => castState.Source.transform.position,
            _ => castState.Source.transform.position,
        };
    }

    private Vector2 GetDirection(CastState castState)
    {
        return _data.GeneratingDirection switch
        {
            PointsGeneratingDirection.Random => Random.insideUnitCircle,
            PointsGeneratingDirection.Forward => (castState.GetTarget() is MovableWorldObject movable) ? movable.TurnDirection : Vector2.down,
            PointsGeneratingDirection.Down => Vector2.down,
            _ => Vector2.down,
        };
    }

    private float GetAngle(int currentTurnIndex)
    {
        return _data.GeneratingOrder switch
        {
            PointsGeneratingOrder.Random => Random.Range(-_data.Angle / 2, _data.Angle / 2),
            PointsGeneratingOrder.Interval => _data.Angle / (_data.Number - 1) * currentTurnIndex - _data.Angle / 2,
            _ => 0
        };
    }

    private float GetDistance()
    {
        return _data.GeneratingDistance switch
        {
            PointsGeneratingDistance.Random => Random.Range(0, _data.Distance),
            PointsGeneratingDistance.Set => _data.Distance,
            _ => 0
        };
    }

    #region Data classes
    [System.Serializable]
    public struct PointsGeneratingData
    {
        public float Distance;
        public float Angle;
        public int Number;
        public PointsGeneratingFulcrum GeneratingFulcrum;
        public PointsGeneratingDistance GeneratingDistance;
        public PointsGeneratingOrder GeneratingOrder;
        public PointsGeneratingDirection GeneratingDirection;
    }

    public enum PointsGeneratingFulcrum
    {
        Target = 1,
        Source = 11,
        SourceWithVisualOffset = 12,
    }

    public enum PointsGeneratingDistance
    {
        Random = 1,
        Set = 11,
    }

    public enum PointsGeneratingOrder
    {
        Random = 1,
        Interval = 11,
    }

    public enum PointsGeneratingDirection
    {
        Random = 1,
        Forward = 11,
        Down = 21,
    }
    #endregion
}
