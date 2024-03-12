using UnityEngine;

public struct PointCastStatePayload : ICastStatePayload
{
    public Vector2 Position { get; set; }
    public float Radius { get; set; }

    public PointCastStatePayload(Vector2 position, float radius)
    {
        Position = position;
        Radius = radius;
    }

    public PointCastStatePayload(Vector2 position) : this(position, 0) { }
}
