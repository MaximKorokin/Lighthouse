using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "ScriptableObjects/Effects/HealEffect", order = 1)]
public class HealEffect : SimpleEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Heal(Value);
        }
    }
}
