using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CastState
{
    public WorldObject InitialSource { get; private set; }
    public WorldObject Source { get; set; }
    public WorldObject Target { get; set; }

    public CastState(WorldObject initialSource, WorldObject source, WorldObject target)
    {
        InitialSource = initialSource;
        Source = source;
        Target = target;
    }
}
