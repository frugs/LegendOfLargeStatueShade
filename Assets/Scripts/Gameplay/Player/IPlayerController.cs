
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    public interface IPlayerController {
        PlayerControls ControlPlayer();
    }

    public struct PlayerControls {
        public readonly Vector2 MovementDirection;
        public readonly bool ShouldAttack;
        public readonly bool ShouldJump;

        public PlayerControls(Vector2 movementDirection, bool shouldAttack, bool shouldJump) {
            ShouldAttack = shouldAttack;
            ShouldJump = shouldJump;
            MovementDirection = movementDirection;
        }
    }
}