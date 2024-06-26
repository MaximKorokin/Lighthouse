using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscapingDashEffect : DashEffect
{
    private static readonly IEnumerable<Vector2> Directions =
        new Vector2[]
        {
            new Vector2(0, 1).normalized,
            new Vector2(1, 1).normalized,
            new Vector2(1, 0).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(0, -1).normalized,
            new Vector2(-1, -1).normalized,
            new Vector2(-1, 0).normalized,
            new Vector2(-1, 0).normalized
        };

    protected override Vector2 GetDirection(CastState castState)
    {
        return Directions.MaxBy(x => EstimateDirection(x, Speed * OverrideTime, castState));
    }

    private static float EstimateDirection(Vector2 direction, float distance, CastState castState)
    {
        var hits = Physics2D
            .RaycastAll(castState.Source.transform.position, direction, distance)
            .Where(x => x.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ObstacleLayerName))
            .ToArray();

        var predictedMovement = hits.Length == 0 ? direction * distance :
            hits.MinBy(x => Vector2.Distance(x.point, castState.Source.transform.position)).point - (Vector2)castState.Source.transform.position;

        return Vector2.Distance((Vector2)castState.Source.transform.position + predictedMovement, castState.Target.transform.position);
    }
}
