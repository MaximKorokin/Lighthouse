using System.Linq;
using UnityEngine;

public class EscapingDashEffect : DashEffect
{
    [SerializeField]
    private bool _normalizeDirection;
    [SerializeField]
    private Vector2[] _directions;

    protected override Vector2 GetDirection(CastState castState)
    {
        var dangerAngle = Vector2.SignedAngle(Vector2.up, castState.GetTargetPosition() - (Vector2)castState.Source.transform.position);
        return _directions
            .OrderBy(x => Random.Range(int.MinValue, int.MaxValue))
            .Select(x => (_normalizeDirection ? x.normalized : x).Rotate(dangerAngle))
            .MaxBy(x => EstimateDirection(x, Speed * OverrideTime, castState));
    }

    private static float EstimateDirection(Vector2 direction, float distance, CastState castState)
    {
        var hits = Physics2D
            .RaycastAll(castState.Source.transform.position, direction, distance)
            .Where(x => !x.collider.isTrigger && x.collider.gameObject.IsObstacle())
            .ToArray();

        var predictedMovement = hits.Length == 0 ? direction * distance :
            hits.MinBy(x => Vector2.Distance(x.point, castState.Source.transform.position)).point - (Vector2)castState.Source.transform.position;

        return Vector2.Distance((Vector2)castState.Source.transform.position + predictedMovement, castState.Target.transform.position);
    }
}
