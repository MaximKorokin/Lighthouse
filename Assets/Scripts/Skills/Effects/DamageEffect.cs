public class DamageEffect : SimpleValueEffect
{
    public override void Invoke(CastState castState)
    {
        var destroyable = castState.GetDestroyableTarget();
        if (destroyable != null)
        {
            destroyable.Damage(Value);
        }
    }
}
