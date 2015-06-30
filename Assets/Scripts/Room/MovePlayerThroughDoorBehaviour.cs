using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class MovePlayerThroughDoorBehaviour : MonoBehaviour {
        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private CurrentRoomBehaviour _currentRoom;

        public void Start() {
            foreach (var door in _currentRoom.CurrentRoomModel.CurrentRoom.Doors) {
                door.PlayerExitedThroughDoor += MovePlayerThroughDoor;
            }
        }

        public void OnDestroy() {
            foreach (var door in _currentRoom.CurrentRoomModel.CurrentRoom.Doors) {
                door.PlayerExitedThroughDoor -= MovePlayerThroughDoor;
            }
        }

        private static void MovePlayerThroughDoor(PlayerBehaviour player, DoorBehaviour door) {
            foreach (var exitDoor in door.Room.Doors) {
                exitDoor.PlayerExitedThroughDoor -= MovePlayerThroughDoor;
            }

            player.PlayerModel.PlayerController = new MoveInDirectionPlayerController(door.ExitDirection);

            Action<PlayerBehaviour, DoorBehaviour> enteredThroughDoorDelegate = null;
            enteredThroughDoorDelegate = (enteringPlayer, entryDoor) => {
                entryDoor.PlayerEnteredThroughDoor -= enteredThroughDoorDelegate;
                enteringPlayer.PlayerModel.PlayerController = InputPlayerController.GetInputPlayerController();

                foreach (var exitDoor in entryDoor.Room.Doors) {
                    exitDoor.PlayerExitedThroughDoor += MovePlayerThroughDoor;
                }
            };

            door.OpposingDoor.PlayerEnteredThroughDoor += enteredThroughDoorDelegate;
        }
    }
}