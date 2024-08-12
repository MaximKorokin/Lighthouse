using UnityEngine;

public class WorldObjectsTriggeringMediator : TriggeringMediator<WorldObject>
{
    protected override Collider2D GetCollider(WorldObject item)
    {
        return item.MainCollider;
    }

    protected override Vector2 GetPosition(WorldObject item)
    {
        return item.transform.position;
    }

    protected override float GetTriggeringRadius(WorldObject item)
    {
        return item.VisionRange;
    }

    protected override bool HasIntersection(WorldObject item, Collider2D collider)
    {
        // todo: maybe use Physics2D.Distance(item.MainCollider, collider);
        return Vector2.Distance(item.transform.position, collider.transform.position) <= item.VisionRange;
    }
}
