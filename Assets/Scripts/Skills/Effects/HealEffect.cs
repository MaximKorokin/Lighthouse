using UnityEngine;

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
