public struct CastState
{
    public WorldObject InitialSource { get; private set; }
    public WorldObject Source { get; set; }
    public WorldObject Target { get; set; }
    public float Cooldown { get; set; }

    public CastState(WorldObject initialSource, WorldObject source, WorldObject target, float cooldown)
    {
        InitialSource = initialSource;
        Source = source;
        Target = target;
        Cooldown = cooldown;
    }

    public CastState(WorldObject initialSource, float cooldown)
    {
        InitialSource = initialSource;
        Source = initialSource;
        Target = initialSource;
        Cooldown = cooldown;
    }
}
