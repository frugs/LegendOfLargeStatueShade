using Assets.Scripts.Room;
using UnityEngine;

namespace Assets.Scripts.Camera {
    public class MainCameraBehaviour : MonoBehaviour {
        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private RoomManagerBehaviour _roomManager;

        private Vector3 currentCameraVelocity;

        public void Update() {
            var mainCamera = UnityEngine.Camera.main;

            var cameraHeight = mainCamera.orthographicSize;
            var cameraWidth = mainCamera.aspect * cameraHeight;

            var roomArea = _roomManager.CurrentRoom.Area;
            
            var playerX = _player.transform.position.x;
            var targetX = cameraWidth > roomArea.width
                ? roomArea.center.x
                : Mathf.Clamp(playerX, roomArea.xMin + cameraWidth, roomArea.xMax - cameraWidth);

            var playerY = _player.transform.position.y;
            var targetY = cameraHeight > roomArea.height
                ? roomArea.center.y
                : Mathf.Clamp(playerY, roomArea.yMin + cameraHeight, roomArea.yMax - cameraHeight);

            var targetDestination = new Vector3(targetX, targetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetDestination, ref currentCameraVelocity, 0.25f);

            //FIXME: hack for when camera should be following player
            if (Mathf.Approximately(targetDestination.x, playerX)) {
                transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
            }

            if (Mathf.Approximately(targetDestination.y, playerY)) {
                transform.position = new Vector3(transform.position.x, playerY, transform.position.z);                
            }
        }
    }
}