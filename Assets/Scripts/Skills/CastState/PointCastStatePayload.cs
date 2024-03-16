using UnityEngine;

public struct PointCastStatePayload : ICastStatePayload
{
    public Vector2 Position { get; set; }

    public PointCastStatePayload(Vector2 position)
    {
        Position = position;
    }
}
