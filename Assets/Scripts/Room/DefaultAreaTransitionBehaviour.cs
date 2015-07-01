using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Camera;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    /// <summary>
    /// <para>
    /// This is the go-to implementation of an AreaTransitionBehaviour, designed for use at the edge of a room (so like a door). 
    /// Alignment (vertical or horizontal) is set in the Unity editor, and then exit direction is calculated from the relative 
    /// positions of this area transition and its room.
    /// </para>
    /// <para>
    /// This class is dependent on being a direct child of the Room it is a door of.
    /// </para>
    /// </summary>
    public class DefaultAreaTransitionBehaviour : AreaTransitionBehaviour {
        [SerializeField] private MainCameraBehaviour _mainCamera;
        [SerializeField] private ScreenFaderBehaviour _screenFader;
        [SerializeField] private AreaTransitionBehaviour _opposingAreaTransition;
        [SerializeField] private Alignment _alignment;

        public override RoomBehaviour Room {
            get { return GetComponentsInParent<RoomBehaviour>(true).First(); }
        }

        public override AreaTransitionBehaviour OpposingAreaTransition {
            get { return _opposingAreaTransition; }
        }

        public override Vector2 ExitDirection {
            get { return _alignment.AlignedDifferenceNormalized(Room.gameObject.transform.position, transform.position); }
        }

        public override Action<PlayerBehaviour, AreaTransitionBehaviour> PlayerExitThroughAreaTransition { get; set; }
        public override Action<AreaTransitionBehaviour> OldAreaFadeOutFinished { get; set; }
        public override Action<AreaTransitionBehaviour> NewAreaFadeInFinished { get; set; }
        public override Action<PlayerBehaviour, AreaTransitionBehaviour> PlayerEntryThroughAreaTransition { get; set; }

        public void Awake() {
            PlayerExitThroughAreaTransition = (player, area) => { };
            OldAreaFadeOutFinished = area => { };
            NewAreaFadeInFinished = area => { };
            PlayerEntryThroughAreaTransition = (player, area) => { };
        }

        public void OnTriggerEnter2D(Collider2D other) {
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null) {
                if (IsPlayerRoomSide(player)) {
                    PlayerExit(player);
                } else {
                    PlayerOnScreen();
                }
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null) {
                if (IsPlayerRoomSide(player)) {
                    PlayerEntry(player);
                } else {
                    PlayerOffscreen(player);
                }
            }
        }

        private bool IsPlayerRoomSide(PlayerBehaviour player) {
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 exitPos = transform.position;

            return _alignment.AlignedDifferenceNormalized(playerPos, exitPos) == ExitDirection;
        }

        private void PlayerExit(PlayerBehaviour player) {
            PlayerExitThroughAreaTransition(player, this);
            player.PlayerModel.PlayerController = new MoveInDirectionPlayerController(ExitDirection);
        }

        private void PlayerOffscreen(PlayerBehaviour player) {
            StartCoroutine(FadeOutCoroutine(player));
            player.PlayerModel.PlayerController = new MoveInDirectionPlayerController(Vector2.zero);
        }

        private IEnumerator FadeOutCoroutine(PlayerBehaviour player) {
            foreach (var frame in _screenFader.FadeToColour(Color.black)) {
                yield return frame;
            }

            Room.Deactivate();

            var opposingRoom = OpposingAreaTransition.Room;
            opposingRoom.gameObject.transform.position = transform.position;

            Vector3 offset = OpposingAreaTransition.ExitDirection * 2;
            player.transform.position = OpposingAreaTransition.transform.position + offset;

            Vector2 cameraTarget = player.transform.position;
            _mainCamera.gameObject.transform.Translate(cameraTarget, _mainCamera.gameObject.transform);

            _mainCamera.CameraReturnedToTrackingPlayer += CameraUpdated(player);

            opposingRoom.Activate();

            OldAreaFadeOutFinished(this);

            // FIXME: Implicit dependency on the fact that the above call updates the current room, which in turn
            //        updates the camera target
            _mainCamera.ResetCamera();
        }

        private void PlayerOnScreen() {
            StartCoroutine(FadeInCoroutine());
        }

        private Action CameraUpdated(PlayerBehaviour player) {
            Action del = null;
            del = () => {
                player.StartCoroutine(FadeInCoroutine());
                _mainCamera.CameraReturnedToTrackingPlayer -= del;
                player.PlayerModel.PlayerController = new MoveInDirectionPlayerController(OpposingAreaTransition.ExitDirection * -1);
            };
            return del;
        }

        private IEnumerator FadeInCoroutine() {
            foreach (var frame in _screenFader.FadeToColour(Color.clear)) {
                yield return frame;
            }
            NewAreaFadeInFinished(this);
        }

        private void PlayerEntry(PlayerBehaviour player) {
            PlayerEntryThroughAreaTransition(player, this);
            player.PlayerModel.PlayerController = new InputPlayerController();
        }
    }
}