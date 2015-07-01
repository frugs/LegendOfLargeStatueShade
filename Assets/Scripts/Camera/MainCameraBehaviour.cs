using System;
using Assets.Scripts.Gameplay.Player;
using Assets.Scripts.Room;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Camera {
    public class MainCameraBehaviour : MonoBehaviour {
        private enum CameraAction {
            TrackPlayer,
            TransitionRooms
        }

        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private CurrentRoomBehaviour _currentRoom;
        [SerializeField] private ScreenFaderBehaviour _screenFader;

        private ICurrentRoomModel _currentRoomModel;

        private Vector3 _currentCameraVelocity;
        private CameraAction _currentCameraAction = CameraAction.TrackPlayer;

        public Action CameraReturnedToTrackingPlayer { get; set; }

        public void Awake() {
            _currentRoomModel = _currentRoom.CurrentRoomModel;
            CameraReturnedToTrackingPlayer += () => { };
        }

        public void Start() {
            _currentRoomModel.CurrentRoomUpdated += UpdateCameraAction;
        }

        public void OnDestroy() {
            _currentRoomModel.CurrentRoomUpdated -= UpdateCameraAction;
        }

        public void Update() {
            var mainCamera = UnityEngine.Camera.main;

            var cameraHeight = mainCamera.orthographicSize * 2;
            var cameraWidth = mainCamera.aspect * cameraHeight;

            var roomArea = _currentRoomModel.CurrentRoom.Area;

            var playerX = _player.transform.position.x;
            var targetX = cameraWidth > roomArea.width
                ? roomArea.center.x
                : Mathf.Clamp(playerX, roomArea.xMin + (cameraWidth / 2), roomArea.xMax - (cameraWidth / 2));

            var playerY = _player.transform.position.y;
            var targetY = cameraHeight > roomArea.height
                ? roomArea.center.y
                : Mathf.Clamp(playerY, roomArea.yMin + (cameraHeight / 2), roomArea.yMax - (cameraHeight / 2));

            var targetDestination = new Vector3(targetX, targetY, transform.position.z);

            switch (_currentCameraAction) {
                case CameraAction.TrackPlayer: {
                    transform.position = Vector3.SmoothDamp(transform.position, targetDestination, ref _currentCameraVelocity, 0.05f);
                    break;
                }

                case CameraAction.TransitionRooms: {
                    transform.position = _screenFader.IsFadedOut 
                        ? targetDestination 
                        : Vector3.SmoothDamp(transform.position, targetDestination, ref _currentCameraVelocity, 0.2f);

                    if (Vector2Util.WithinRange(targetDestination, transform.position, 0.01f)) {
                        CameraReturnedToTrackingPlayer();
                        _currentCameraAction = CameraAction.TrackPlayer;
                    }
                    break;
                }
            }
        }

        private void UpdateCameraAction(RoomBehaviour fromRoom, RoomBehaviour toRoom) {
            _currentCameraAction = CameraAction.TransitionRooms;
        }
    }
}