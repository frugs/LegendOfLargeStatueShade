using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System;
    using Player;

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

        private void MovePlayerThroughDoor(PlayerBehaviour player, DoorBehaviour door) {
            var destinationRoom = _roomMap.GetAdjacentRoom(door);
            AlignRoom(destinationRoom, door);
            destinationRoom.Activate();
            player.PlayerController = new RoomTransitionPlayerController(door);

            Action<PlayerBehaviour, DoorBehaviour> del = null;
            del = delegate(PlayerBehaviour enteringPlayer, DoorBehaviour opposingDoor) {
                CurrentRoom.Deactivate();
                opposingDoor.PlayerEnteredThroughDoor -= del;
                CurrentRoom = destinationRoom;
                enteringPlayer.SetDefaultPlayerController();
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