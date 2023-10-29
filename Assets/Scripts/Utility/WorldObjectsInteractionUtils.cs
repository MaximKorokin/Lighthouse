using System.Collections.Generic;
using System.Linq;

public static class WorldObjectsInteractionUtils
{
    public static WorldObject[] GetValidTargets(this IEnumerable<WorldObject> worldObjects, WorldObject source)
    {
        var validator = source.GetComponent<ValidatorBase>();
        if (validator == null)
        {
            return worldObjects.ToArray();
        }
        else
        {
            return worldObjects.Where(x => validator.IsValidTarget(x)).ToArray();
        }
    }
}
