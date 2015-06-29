using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System;

    public abstract class DoorBehaviour : MonoBehaviour {
        public abstract DoorBehaviour OpposingDoor { get; }

        public abstract RoomBehaviour Room { get; }

        public abstract Vector2 AnchorPosition { get; }
        public abstract Vector2 ExitDirection { get; }

        public abstract Action<PlayerBehaviour, DoorBehaviour> PlayerEnteredThroughDoor { get; set; }
        public abstract Action<PlayerBehaviour, DoorBehaviour> PlayerExitedThroughDoor { get; set; }
    }
}