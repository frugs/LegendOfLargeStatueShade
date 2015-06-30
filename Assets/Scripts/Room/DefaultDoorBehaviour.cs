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
                var self = Room.Area.center;
                var other = OpposingDoor.Room.Area.center;

                return CalculateRelativeDirection(self, other, _alignment);
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
            if (player != null && PlayerDirectionEqualsExitDirection(player)) {
                PlayerExitedThroughDoor(player, this);
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null && PlayerDirectionEqualsExitDirection(player)) {
                PlayerEnteredThroughDoor(player, this);
            }
        }

        private bool PlayerDirectionEqualsExitDirection(PlayerBehaviour player) {
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 exitPos = transform.position;

            return CalculateRelativeDirection(playerPos, exitPos, _alignment) == ExitDirection;
        }

        private static Vector2 CalculateRelativeDirection(Vector2 self, Vector2 other, Alignment alignment) {
            var selfValue = alignment == Alignment.Horizontal ? self.x : self.y;
            var otherValue = alignment == Alignment.Horizontal ? other.x : other.y;

            return alignment == Alignment.Horizontal
                ? new Vector2(otherValue - selfValue, 0).normalized
                : new Vector2(0, otherValue - selfValue).normalized;
        }
    }
}