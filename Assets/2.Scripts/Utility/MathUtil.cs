using UnityEngine;

public static class MathUtil
{
    public static Vector2 Projection(this Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a, b) / b.sqrMagnitude * b;
    }
}
