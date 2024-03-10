using UnityEngine;

public class ChildZoneEffect : ZoneEffect
{
    protected override PeriodicActor CreateZone(CastState castState)
    {
        var zone = base.CreateZone(castState);
        ChildZoneToTarget(zone, castState);
        return zone;
    }

    private void ChildZoneToTarget(PeriodicActor zone, CastState castState)
    {
        zone.transform.parent = castState.Target.transform;

        if (castState.Target is MovableWorldObject movable)
        {
            MovableDirectionSet(movable.Direction);
            movable.DirectionSet += MovableDirectionSet;
            zone.WorldObject.Destroyed += _ => movable.DirectionSet -= MovableDirectionSet;
        }

        void MovableDirectionSet(Vector2 _)
        {
            var turnDirection = movable.TurnDirection.normalized;
            if (zone.WorldObject is MovableWorldObject zoneMovable)
            {
                zoneMovable.Direction = turnDirection;
            }
            zone.transform.localPosition = turnDirection * DistanceFromParent;
        }
    }
}
