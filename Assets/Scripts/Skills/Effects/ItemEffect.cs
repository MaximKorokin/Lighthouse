public class ItemEffect : ComplexEffect
{
    public override void Invoke(CastState castState)
    {
        base.Invoke(castState);
        if (castState.Source is DestroyableWorldObject destroyable)
        {
            destroyable.DestroyWorldObject();
        }
    }
}
