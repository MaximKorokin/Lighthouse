using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Physics2DUtils
{
    public static IEnumerable<WorldObject> GetWorldObjectsInRadius(Vector2 position, float radius)
    {
        return Physics2D.OverlapCircleAll(position, radius)
            .Where(x => x != null && !x.isTrigger)
            .Select(x => x.GetComponent<WorldObject>())
            .Where(x => x != null)
            .Distinct();
    }

    public static bool IsReachable(this GameObject obj, Vector2 origin, Vector2 direction, float distance)
    {
        return Physics2D.RaycastAll(origin, direction, distance)
                .TakeWhile(x => x.transform.gameObject != obj)
                .OrderBy(x => x.distance).Any(x => x.transform.gameObject.GetComponent<Obstacle>() != null);
    }
}
