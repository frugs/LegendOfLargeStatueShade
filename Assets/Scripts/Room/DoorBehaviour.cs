using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System;
    using Player;

    public abstract class DoorBehaviour : MonoBehaviour {
        public abstract Id<DoorBehaviour> Id { get; }
        public abstract Id<DoorBehaviour> OpposingDoorId { get; }

        public abstract RoomBehaviour Room { get; }

        public abstract Vector2 AnchorPosition { get; }
        public abstract Vector2 ExitDirection { get; }

        public abstract Action<PlayerBehaviour, DoorBehaviour> PlayerEnteredThroughDoor { get; set; }
        public abstract Action<PlayerBehaviour, DoorBehaviour> PlayerExitedThroughDoor { get; set; }
    }
}