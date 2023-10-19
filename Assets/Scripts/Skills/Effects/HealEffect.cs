using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "ScriptableObjects/Effects/HealEffect", order = 1)]
public class HealEffect : SimpleEffect
{
    public override void Invoke(WorldObject source, WorldObject target)
    {
        if (target is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Heal(Value);
        }
    }
}
