using UnityEngine;

namespace Tiger {
    public static class Vector2Extensions
    {
        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }
        
        public static Vector2 Add(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(vector.x + (x ?? 0f), vector.y + (y ?? 0f));
        }
        
        public static Vector2 Multiply(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(vector.x * (x ?? 1f), vector.y * (y ?? 1f));
        }
    }
}