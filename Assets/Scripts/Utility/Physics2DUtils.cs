using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Physics2DUtils
{
    public static IEnumerable<WorldObject> GetWorldObjectsInRadius(Vector2 position, float radius)
    {
        return Physics2D.OverlapCircleAll(position, radius)
            .Distinct()
            .SelectMany(x => x.GetComponents<Collider2D>())
            .Where(x => x != null && !x.isTrigger)
            .Select(x => x.GetComponent<WorldObject>())
            .Where(x => x != null);
    }
}
