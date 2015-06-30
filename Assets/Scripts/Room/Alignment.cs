using UnityEngine;

namespace Assets.Scripts.Room {
    public enum Alignment {
        Vertical,
        Horizontal
    }

    public static class AlignmentUtil {
        public static Vector2 AlignedDifferenceNormalized(this Alignment alignment, Vector2 origin, Vector2 target) {
            var originValue = alignment == Alignment.Horizontal ? origin.x : origin.y;
            var targetValue = alignment == Alignment.Horizontal ? target.x : target.y;

            return alignment == Alignment.Horizontal
                ? new Vector2(targetValue - originValue, 0).normalized
                : new Vector2(0, targetValue - originValue).normalized;
        }
    }
}