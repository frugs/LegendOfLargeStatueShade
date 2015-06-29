using UnityEngine;

namespace Assets.Scripts.Util {
    public static class Vector2Util {
        public static bool WithinRange(Vector2 a, Vector2 b, float range) {
            return (a - b).magnitude <= range;
        }
    }
}