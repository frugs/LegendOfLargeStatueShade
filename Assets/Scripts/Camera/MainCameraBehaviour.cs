using System;
using Assets.Scripts.Room;
using UnityEngine;

namespace Assets.Scripts.Camera {
    using Player;

    public class MainCameraBehaviour : MonoBehaviour {

        private enum CameraAction { TrackPlayer, TransitionRooms }

        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private RoomManagerBehaviour _roomManager;

        private Vector3 _currentCameraVelocity;
        private CameraAction _currentCameraAction = CameraAction.TrackPlayer;

        public Action CameraTransistionedRooms { get; set; }

        public void Start() {
            // FIXME: This never gets unregistered! register/unregister on enable/disable instead
            _roomManager.CurrentRoomUpdated += room => {
                _currentCameraAction = CameraAction.TransitionRooms;
            };
        }

        public void Update() {
            var mainCamera = UnityEngine.Camera.main;

            var cameraHeight = mainCamera.orthographicSize * 2;
            var cameraWidth = mainCamera.aspect * cameraHeight;

            var roomArea = _roomManager.CurrentRoom.Area;

            var playerX = _player.transform.position.x;
            var targetX = cameraWidth > roomArea.width
                ? roomArea.center.x
                : Mathf.Clamp(playerX, roomArea.xMin + (cameraWidth / 2), roomArea.xMax - (cameraWidth / 2));

            var playerY = _player.transform.position.y;
            var targetY = cameraHeight > roomArea.height
                ? roomArea.center.y
                : Mathf.Clamp(playerY, roomArea.yMin + (cameraHeight / 2), roomArea.yMax - (cameraHeight / 2));

            switch (_currentCameraAction) {
                case CameraAction.TrackPlayer: {
                    _currentCameraVelocity = Vector3.zero;
                    transform.position = new Vector3(targetX, targetY, transform.position.z);
                    break; 
                }

                case CameraAction.TransitionRooms: {
                    var targetDestination = new Vector3(targetX, targetY, transform.position.z);
                    transform.position = Vector3.SmoothDamp(transform.position, targetDestination, ref _currentCameraVelocity, 0.25f);

                    if (targetDestination == transform.position) {
                        CameraTransistionedRooms();
                        _currentCameraAction = CameraAction.TrackPlayer;
                    }
                    break;
                }
            }
        }
    }
}