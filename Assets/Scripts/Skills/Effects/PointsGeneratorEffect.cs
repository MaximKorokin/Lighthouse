using UnityEngine;

public class PointsGeneratorEffect : ComplexEffect
{
    [SerializeField]
    private bool _baseOnTarget;
    [SerializeField]
    private int _pointsNumber;
    [SerializeField]
    private float _maxRandomDistance;

    public override void Invoke(CastState castState)
    {
        castState.TargetingType = TargetingType.Point;
        var initialPosition = _baseOnTarget ? castState.Target.transform.position : castState.Source.transform.position;
        for (int i = 0; i < _pointsNumber; i++)
        {
            castState.Payload = new PointCastStatePayload(initialPosition + (Vector3)Random.insideUnitCircle * Random.Range(0, _maxRandomDistance));
            base.Invoke(castState);
        }
    }
}
