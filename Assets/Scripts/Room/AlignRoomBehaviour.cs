using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class AlignRoomBehaviour : MonoBehaviour {
        [SerializeField] private CurrentRoomBehaviour _currentRoom;

        public void Start() {
            foreach (var door in _currentRoom.CurrentRoomModel.CurrentRoom.Doors) {
                door.PlayerExitedThroughDoor += AlignRoom;
            }

            _currentRoom.CurrentRoomModel.CurrentRoomUpdated += RegisterAlignRoom;
        }

        public void OnDestroy() {
            _currentRoom.CurrentRoomModel.CurrentRoomUpdated -= RegisterAlignRoom;
        }

        private static void RegisterAlignRoom(RoomBehaviour fromRoom, RoomBehaviour toRoom) {
            foreach (var door in fromRoom.Doors) {
                door.PlayerExitedThroughDoor -= AlignRoom;
            }

            foreach (var door in toRoom.Doors) {
                door.PlayerExitedThroughDoor += AlignRoom;
            }
        }

        private static void AlignRoom(PlayerBehaviour player, DoorBehaviour exitDoor) {
            var entryDoor = exitDoor.OpposingDoor;
            var delta = exitDoor.AnchorPosition - entryDoor.AnchorPosition;
            entryDoor.Room.gameObject.transform.Translate(delta);
        }
    }
}