using System;
using Assets.Scripts.Camera;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class RoomActivationBehaviour : MonoBehaviour {

        [SerializeField] private CurrentRoomBehaviour _currentRoom;
        [SerializeField] private MainCameraBehaviour _mainCamera;
        
        private Action _deactivateRoom = () => { };

        public void Start() {
            _currentRoom.CurrentRoomModel.CurrentRoomUpdated += ActivateRoom;
            _currentRoom.CurrentRoomModel.CurrentRoomUpdated += RegisterDeactivateRoomCallback;
        }

        public void OnDestroy() {
            _currentRoom.CurrentRoomModel.CurrentRoomUpdated -= ActivateRoom;
            _currentRoom.CurrentRoomModel.CurrentRoomUpdated -= RegisterDeactivateRoomCallback;
        }

        private static void ActivateRoom(RoomBehaviour fromRoom, RoomBehaviour toRoom) {
            toRoom.Activate();
        }

        private void RegisterDeactivateRoomCallback(RoomBehaviour fromRoom, RoomBehaviour toRoom) {
            _mainCamera.CameraReturnedToTrackingPlayer -= _deactivateRoom;

            _deactivateRoom = () => {
                fromRoom.Deactivate();
                _mainCamera.CameraReturnedToTrackingPlayer -= _deactivateRoom;
            };
            _mainCamera.CameraReturnedToTrackingPlayer += _deactivateRoom;
        }
    }
}