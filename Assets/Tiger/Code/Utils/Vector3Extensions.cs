using UnityEngine;

namespace Tiger {
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x + (x ?? 0f), vector.y + (y ?? 0f), vector.z + (z ?? 0f));
        }
        
        public static Vector3 Multiply(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x * (x ?? 1f), vector.y * (y ?? 1f), vector.z * (z ?? 1f));
        }
    }
}