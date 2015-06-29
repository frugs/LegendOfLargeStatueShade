using System;
using Assets.Scripts.Camera;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class RoomManagerBehaviour : MonoBehaviour {
        [SerializeField] private RoomBehaviour _defaultRoom;
        [SerializeField] private MainCameraBehaviour _mainCamera;

        private RoomBehaviour _currentRoom;
        private Action _cameraRoomTransitionDelegate = () => { };

        public RoomBehaviour CurrentRoom {
            get { return _currentRoom; }
            private set {
                _currentRoom = value;
                CurrentRoomUpdated(value);
            }
        }

        public Action<RoomBehaviour> CurrentRoomUpdated { get; set; }

        public void Awake() {
            _currentRoom = _defaultRoom;
            CurrentRoomUpdated = room => { };
        }

        public void Start() {
            CurrentRoom.Activate();
            RegisterPlayerExitThroughDoorCallbacks(CurrentRoom);
        }

        private void RegisterPlayerExitThroughDoorCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor += MovePlayerThroughDoor;
            }
        }

        private void UnregisterPlayerExitThroughDoorCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor -= MovePlayerThroughDoor;
            }
        }

        private void MovePlayerThroughDoor(PlayerBehaviour player, DoorBehaviour door) {
            var destinationRoom = door.OpposingDoor.Room;
            AlignRoom(destinationRoom, door);
            destinationRoom.Activate();
            player.PlayerModel.PlayerController = new RoomTransitionPlayerController(door);

            var fromRoom = CurrentRoom;
            CurrentRoom = destinationRoom;
            _mainCamera.CameraTransistionedRooms -= _cameraRoomTransitionDelegate;
            _cameraRoomTransitionDelegate = () => {
                fromRoom.Deactivate();
                _mainCamera.CameraTransistionedRooms -= _cameraRoomTransitionDelegate;
            };
            _mainCamera.CameraTransistionedRooms += _cameraRoomTransitionDelegate;

            Action<PlayerBehaviour, DoorBehaviour> enteredThroughDoorDelegate = null;
            enteredThroughDoorDelegate = delegate(PlayerBehaviour enteringPlayer, DoorBehaviour opposingDoor) {
                opposingDoor.PlayerEnteredThroughDoor -= enteredThroughDoorDelegate;
                enteringPlayer.PlayerModel.PlayerController = InputPlayerController.GetInputPlayerController();
                RegisterPlayerExitThroughDoorCallbacks(CurrentRoom);
            };
            door.OpposingDoor.PlayerEnteredThroughDoor += enteredThroughDoorDelegate;

            UnregisterPlayerExitThroughDoorCallbacks(fromRoom);
        }

        private static void AlignRoom(RoomBehaviour roomToAlign, DoorBehaviour doorToAlignTo) {
            var delta = doorToAlignTo.AnchorPosition - doorToAlignTo.OpposingDoor.AnchorPosition;
            roomToAlign.gameObject.transform.Translate(delta);
        }
    }
}