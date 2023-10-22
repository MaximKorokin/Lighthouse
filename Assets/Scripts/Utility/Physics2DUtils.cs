using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Physics2DUtils
{
    public static IEnumerable<WorldObject> GetWorldObjectsInRadius(Vector2 position, float radius)
    {
        return Physics2D.OverlapCircleAll(position, radius)
            .Select(c => c.GetComponent<WorldObject>())
            .Where(w => w != null);
    }
}
