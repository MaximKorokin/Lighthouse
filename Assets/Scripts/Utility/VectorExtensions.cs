using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 Rotate(this Vector2 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }

    public static Vector3 Rotate2D(this Vector3 vector, float angle)
    {
        return ((Vector2)vector).Rotate(angle);
    }
}
