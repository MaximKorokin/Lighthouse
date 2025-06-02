using System.Collections.Generic;

// todo: rename to EffectState / EffectContext
// todo: maybe change it to class
public struct CastState
{
    /// <summary>
    /// TargetingType.Target is set by default
    /// </summary>
    public TargetingType TargetingType { get; set; }
    public WorldObject InitialSource { get; private set; }
    public WorldObject Source { get; set; }
    public WorldObject Target { get; set; }
    public ICastStatePayload Payload { get; set; }
    public readonly DataStore<string> Cache;

    public CastState(WorldObject initialSource, WorldObject source, WorldObject target)
    {
        TargetingType = TargetingType.Target;
        InitialSource = initialSource;
        Source = source;
        Target = target;
        Payload = null;
        Cache = new();
    }

    public CastState(WorldObject initialSource)
    {
        TargetingType = TargetingType.Target;
        InitialSource = initialSource;
        Source = initialSource;
        Target = initialSource;
        Payload = null;
        Cache = new();
    }
}

public enum TargetingType
{
    Source = 2,
    Target = 3,
    Point = 4
}
