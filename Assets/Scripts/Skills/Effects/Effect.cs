using System;

[Serializable]
public abstract class Effect
{
    public abstract void Invoke(CastState castState);

    public void Invoke(WorldObject source) => Invoke(new CastState(source, 0));
}
