using UnityEngine;

public class DamageEffect : SimpleEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Damage(Value);
        }
    }
}
