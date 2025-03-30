using UnityEngine;

public class ChildZoneEffect : ZoneEffect
{
    protected override PeriodicActor CreateZone(CastState castState)
    {
        var zone = base.CreateZone(castState);
        ChildZone(zone, castState);
        return zone;
    }

    private void ChildZone(PeriodicActor zone, CastState castState)
    {
        var target = castState.GetTarget();
        zone.transform.parent = target.transform;

        if (target is MovableWorldObject movable && DistanceFromParent != 0)
        {
            MovableDirectionSet(movable.Direction);
            movable.DirectionSet += MovableDirectionSet;
            zone.WorldObject.OnDestroyed(() => movable.DirectionSet -= MovableDirectionSet);
        }

        void MovableDirectionSet(Vector2 _)
        {
            var turnDirection = movable.TurnDirection.normalized;
            if (zone.WorldObject is MovableWorldObject zoneMovable)
            {
                zoneMovable.Direction = turnDirection;
            }

            zone.transform.localPosition = turnDirection * DistanceFromParent + movable.VisualSize * Vector2.up * 0.5f;
        }
    }
}
