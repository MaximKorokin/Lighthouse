using UnityEngine;

public abstract class ProjecltileManipulator : ManipulatorBase
{
    public override void Manipulate(WorldObject worldObject)
    {
        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        Debug.Log($"Projectile {name} hit {destroyableWorldObject.name}");
    }
}
