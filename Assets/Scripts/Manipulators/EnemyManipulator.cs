using UnityEngine;

public class EnemyManipulator : ManipulatorBase
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is PlayerCreature playerCreature && playerCreature.IsAlive;
    }

    public override void Manipulate(WorldObject worldObject)
    {
        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        Debug.Log($"Enemy {name} hit {destroyableWorldObject.name}");
    }
}
