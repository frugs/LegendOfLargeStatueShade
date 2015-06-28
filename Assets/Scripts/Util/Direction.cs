using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Util {
    public enum Direction {
        Up, Down, Left, Right    
    }

    static class DirectionUtil {
        public static Vector2 ToVector2(this Direction dir) {
            switch (dir) {
                case Direction.Up: 
                    return Vector2.up;
                case Direction.Left:
                    return Vector2.left;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Right:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }

        public static Direction FromVector2(Vector2 vector) {
            var normalized = vector.normalized;
            
            var upVal = (normalized - Vector2.up).magnitude;
            var leftVal = (normalized - Vector2.left).magnitude;
            var rightVal = (normalized - Vector2.right).magnitude;
            var downVal = (normalized - Vector2.down).magnitude;

            var dirVal = new List<float> {upVal, leftVal, rightVal, downVal}.Min();

            if (Mathf.Approximately(upVal, dirVal)) {
                return Direction.Up;
            }
            
            if (Mathf.Approximately(leftVal, dirVal)) {
                return Direction.Left;
            }

            if (Mathf.Approximately(rightVal, dirVal)) {
                return Direction.Right;
            }

            return Direction.Down;
        }
    }
}