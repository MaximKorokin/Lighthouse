using UnityEngine;

public class ZoneEffect : ComplexEffect
{
    [field: SerializeField]
    public PeriodicActor Zone { get; private set; }
    [field: SerializeField]
    public bool ChildToTarget { get; private set; }
    [field: SerializeField]
    public float DistanceFromSource { get; private set; }

    public override void Invoke(CastState castState)
    {
        CreateZone(castState);
    }

    private void CreateZone(CastState castState)
    {
        var zone = Object.Instantiate(Zone);
        zone.SetEffects(Effects, castState);
        if (ChildToTarget)
        {
            zone.transform.parent = castState.Target.transform;
        }
        if (castState.Target is MovableWorldObject movable)
        {
            MovableDirectionSet(movable.Direction);
            movable.DirectionSet += MovableDirectionSet;
            zone.WorldObject.Destroyed += _ => movable.DirectionSet -= MovableDirectionSet;
        }
        else
        {
            zone.transform.position = castState.Target.transform.position;
        }

        void MovableDirectionSet(Vector2 _)
        {
            var turnDirection = movable.TurnDirection.normalized;
            if (zone.WorldObject is MovableWorldObject zoneMovable)
            {
                zoneMovable.Direction = turnDirection;
            }
            zone.transform.localPosition = turnDirection * DistanceFromSource;
        }
    }
}
