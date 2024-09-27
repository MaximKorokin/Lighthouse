using UnityEngine;

public class HealEffect : SimpleValueEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Heal(Value);
        }
    }
}
