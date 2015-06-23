
namespace Assets.Scripts.Player {
    using UnityEngine;

    public interface IPlayerController {
        PlayerControls ControlPlayer();
    }

    public struct PlayerControls {
        public readonly Vector2 MovementDirection;
        public readonly bool ShouldAttack;

        public PlayerControls(Vector2 movementDirection, bool shouldAttack) {
            ShouldAttack = shouldAttack;
            MovementDirection = movementDirection;
        }
    }
}