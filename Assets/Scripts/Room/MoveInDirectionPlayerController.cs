
using Assets.Scripts.Gameplay.Player;

namespace Assets.Scripts.Room {
    using UnityEngine;

    class MoveInDirectionPlayerController : IPlayerController {
        private readonly Vector2 _movementDirection;

        public MoveInDirectionPlayerController(Vector2 movementDirection) {
            _movementDirection = movementDirection;
        }

        public PlayerControls ControlPlayer() {
            return new PlayerControls(_movementDirection, false, false);
        }
    }
}
