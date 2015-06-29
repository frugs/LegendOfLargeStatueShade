using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    /// <summary>
    /// <para>
    /// This is the go-to implementation of a DoorBehaviour, where the anchor location
    /// is simply the position of the gameobject transform. Alignment (vertical or horizontal) is set
    /// in the Unity editor, and then exit direction is calculated from the relative positions of this
    /// door and the opposing door's parent rooms.
    /// </para>
    /// <para>
    /// This class is dependent on being a direct child of the Room it is a door of.
    /// </para>
    /// </summary>
    [RequireComponent(typeof (BoxCollider2D))]
    public class DefaultDoorBehaviour : DoorBehaviour {
        public enum Alignment {
            Vertical,
            Horizontal
        }

        [SerializeField] private DoorBehaviour _opposingDoor;
        [SerializeField] private Alignment _alignment;

        private Action<PlayerBehaviour, DoorBehaviour> _playerExitedThroughDoor = (player, door) => { };
        private Action<PlayerBehaviour, DoorBehaviour> _playerEnteredThroughDoor = (player, door) => { };

        public override DoorBehaviour OpposingDoor {
            get { return _opposingDoor; }
        }

        public override RoomBehaviour Room {
            get { return transform.parent.GetComponent<RoomBehaviour>(); }
        }

        public override Vector2 AnchorPosition {
            get { return transform.position; }
        }

        public override Vector2 ExitDirection {
            get {
                float self = _alignment == Alignment.Horizontal 
                    ? Room.Area.center.x 
                    : Room.Area.center.y;
                float opposing = _alignment == Alignment.Horizontal
                    ? OpposingDoor.Room.Area.center.x
                    : OpposingDoor.Room.Area.center.y;

                float direction = opposing - self;
                return _alignment == Alignment.Horizontal
                    ? new Vector2(direction, 0).normalized
                    : new Vector2(0, direction).normalized;
            }
        }

        public override Action<PlayerBehaviour, DoorBehaviour> PlayerEnteredThroughDoor {
            get { return _playerEnteredThroughDoor; }
            set { _playerEnteredThroughDoor = value; }
        }

        public override Action<PlayerBehaviour, DoorBehaviour> PlayerExitedThroughDoor {
            get { return _playerExitedThroughDoor; }
            set { _playerExitedThroughDoor = value; }
        }

        public void OnTriggerEnter2D(Collider2D other) {
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null) {
                PlayerExitedThroughDoor(player, this);
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null) {
                PlayerEnteredThroughDoor(player, this);
            }
        }
    }
}