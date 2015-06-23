using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System;

    public abstract class DoorBehaviour : MonoBehaviour {
        public abstract Id<DoorBehaviour> Id { get; }
        public abstract Id<DoorBehaviour> OpposingDoorId { get; }

        public abstract RoomBehaviour Room { get; }

        public abstract Vector2 AnchorPosition { get; }

        public abstract Action<DoorBehaviour> PlayerEnteredThroughDoor { get; set; }
        public abstract Action<DoorBehaviour> PlayerExitedThroughDoor { get; set; }
    }
}