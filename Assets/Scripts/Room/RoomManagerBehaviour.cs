using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System;

    public class RoomManagerBehaviour : MonoBehaviour {
        [SerializeField] private RoomBehaviour _defaultRoom;
        
        private readonly RoomMap _roomMap = new RoomMap();

        public RoomBehaviour CurrentRoom { get; private set; }

        public void Awake() {
            foreach (var room in GetComponentsInChildren<RoomBehaviour>()) {
                foreach (var door in room.Doors) {
                    door.PlayerExitedThroughDoor += MovePlayerThroughDoor;
                }

                _roomMap.AddRoom(room);
                room.Deactivate();
            }
        }

        public void Start() {
            CurrentRoom = _roomMap.GetRoom(_defaultRoom.Id);
            CurrentRoom.Activate();
        }

        private void MovePlayerThroughDoor(DoorBehaviour door) {
            var destinationRoom = _roomMap.GetAdjacentRoom(door);
            AlignRoom(destinationRoom, door);
            destinationRoom.Activate();

            Action<DoorBehaviour> del = null;
            del = delegate(DoorBehaviour opposingDoor) {
                CurrentRoom.Deactivate();
                opposingDoor.PlayerEnteredThroughDoor -= del;
                CurrentRoom = destinationRoom;
            };
            destinationRoom.GetDoor(door.OpposingDoorId).PlayerEnteredThroughDoor += del;
        }

        private static void AlignRoom(RoomBehaviour roomToAlign, DoorBehaviour doorToAlignTo) {
            var opposingDoor = roomToAlign.GetDoor(doorToAlignTo.OpposingDoorId);
            var delta = doorToAlignTo.AnchorPosition - opposingDoor.AnchorPosition;
            roomToAlign.gameObject.transform.Translate(delta);
        }
    }
}