
using Assets.Scripts.Gameplay.Player;

namespace Assets.Scripts.Room {
    using UnityEngine;

    class RoomTransitionPlayerController : IPlayerController {
        private readonly Vector2 _movementDirection;

        public RoomTransitionPlayerController(DoorBehaviour exitDoor) {
            _movementDirection = exitDoor.ExitDirection;
        }

        public PlayerControls ControlPlayer() {
            return new PlayerControls(_movementDirection, false);
        }
    }
}
