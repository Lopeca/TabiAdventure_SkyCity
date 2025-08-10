using UnityEngine;

namespace _2.Scripts.ExtensionMethods
{
    public static class VectorExtension
    {
        public static Vector2 RotateVector(this Vector2 v, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);

            float x = v.x * cos - v.y * sin;
            float y = v.x * sin + v.y * cos;

            return new Vector2(x, y);
        }
    }
}