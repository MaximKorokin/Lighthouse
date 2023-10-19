using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "ScriptableObjects/Effects/DamageEffect", order = 1)]
public class DamageEffect : SimpleEffect
{
    public override void Invoke(WorldObject source, WorldObject target)
    {
        if (target is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Damage(Value);
        }
    }
}
