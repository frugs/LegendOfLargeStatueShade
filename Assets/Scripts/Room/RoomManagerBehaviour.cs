using System;
using Assets.Scripts.Camera;
using Assets.Scripts.Gameplay.Player;
using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    /// <summary>
    /// <para>
    /// This class is dependent on having all the Rooms it is managing attached
    /// to child objects of the gameobject this behaviour is attached to.
    /// </para>
    /// </summary>
    public class RoomManagerBehaviour : MonoBehaviour {
        [SerializeField] private RoomBehaviour _defaultRoom;
        [SerializeField] private MainCameraBehaviour _mainCamera;

        private readonly RoomMap _roomMap = new RoomMap();

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
            foreach (var room in GetComponentsInChildren<RoomBehaviour>()) {
                foreach (var door in room.Doors) {
                    // FIXME: This never gets unregistered! register/unregister on enable/disable instead
                    door.PlayerExitedThroughDoor += MovePlayerThroughDoor;
                }

                _roomMap.AddRoom(room);
                room.Deactivate();
            }

            _currentRoom = _defaultRoom;
            CurrentRoomUpdated = room => { };
            CurrentRoom.Activate();
        }

        private void MovePlayerThroughDoor(PlayerBehaviour player, DoorBehaviour door) {
            var destinationRoom = _roomMap.GetAdjacentRoom(door);
            AlignRoom(destinationRoom, door);
            destinationRoom.Activate();
            player.PlayerController = new RoomTransitionPlayerController(door);

            var fromRoom = CurrentRoom;
            _mainCamera.CameraTransistionedRooms -= _cameraRoomTransitionDelegate;
            _cameraRoomTransitionDelegate = () => {
                fromRoom.Deactivate();
                _mainCamera.CameraTransistionedRooms -= _cameraRoomTransitionDelegate;
            };
            _mainCamera.CameraTransistionedRooms += _cameraRoomTransitionDelegate;

            Action<PlayerBehaviour, DoorBehaviour> enteredThroughDoorDelegate = null;
            enteredThroughDoorDelegate = delegate(PlayerBehaviour enteringPlayer, DoorBehaviour opposingDoor) {
                opposingDoor.PlayerEnteredThroughDoor -= enteredThroughDoorDelegate;
                CurrentRoom = destinationRoom;
                enteringPlayer.SetDefaultPlayerController();
            };
            destinationRoom.GetDoor(door.OpposingDoorId).PlayerEnteredThroughDoor += enteredThroughDoorDelegate;
        }

        private static void AlignRoom(RoomBehaviour roomToAlign, DoorBehaviour doorToAlignTo) {
            var opposingDoor = roomToAlign.GetDoor(doorToAlignTo.OpposingDoorId);
            var delta = doorToAlignTo.AnchorPosition - opposingDoor.AnchorPosition;
            roomToAlign.gameObject.transform.Translate(delta);
        }
    }
}