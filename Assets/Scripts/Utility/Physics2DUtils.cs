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

    public static bool IsBlockedByObstacle(this GameObject obj, Vector2 origin, Vector2 direction, float distance, IEnumerable<GameObject> objectsToSkip = null)
    {
        objectsToSkip ??= Enumerable.Empty<GameObject>();

        return Physics2D.RaycastAll(origin, direction, distance)
            .Where(x => !x.collider.isTrigger)
            .OrderBy(x => x.distance)
            .TakeWhile(x => x.transform.gameObject != obj)
            .Any(x => !objectsToSkip.Contains(x.transform.gameObject) && x.transform.gameObject.IsObstacle());
    }
}
