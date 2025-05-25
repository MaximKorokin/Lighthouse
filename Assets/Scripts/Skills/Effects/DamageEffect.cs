public class DamageEffect : SimpleValueEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.GetTarget() is DestroyableWorldObject destroyableWorldObject)
        {
            destroyableWorldObject.Damage(Value);
        }
    }
}
