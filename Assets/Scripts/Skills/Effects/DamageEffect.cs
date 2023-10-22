using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "ScriptableObjects/Effects/DamageEffect", order = 1)]
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
