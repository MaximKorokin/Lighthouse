using UnityEngine;

public struct CastState
{
    public WorldObject InitialSource { get; private set; }
    public WorldObject Source { get; set; }
    public WorldObject Target { get; set; }
    public Vector2? TargetPosition { get; set; }

    public CastState(WorldObject initialSource, WorldObject source, WorldObject target)
    {
        InitialSource = initialSource;
        Source = source;
        Target = target;
        TargetPosition = null;
    }

    public CastState(WorldObject initialSource)
    {
        InitialSource = initialSource;
        Source = initialSource;
        Target = initialSource;
        TargetPosition = null;
    }
}
