using System.Collections.Generic;
using System.Linq;

public static class WorldObjectsInteractionUtils
{
    public static IEnumerable<WorldObject> GetValidTargets(this IEnumerable<WorldObject> worldObjects, WorldObject source)
    {
        var validator = source.GetComponent<ValidatorBase>();
        if (validator == null)
        {
            return worldObjects;
        }
        else
        {
            return worldObjects.Where(x => validator.IsValidTarget(x));
        }
    }

    public static bool IsValidTarget(this WorldObject worldObject, WorldObject source)
    {
        var validator = source.GetComponent<ValidatorBase>();
        if (validator == null)
        {
            return true;
        }
        else
        {
            return validator.IsValidTarget(worldObject);
        }
    }
}
