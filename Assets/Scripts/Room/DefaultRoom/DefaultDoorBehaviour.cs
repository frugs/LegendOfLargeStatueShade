using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    using Player;

    [RequireComponent(typeof (BoxCollider2D))]
    public class DefaultDoorBehaviour : DoorBehaviour {

        [SerializeField] private string _id;
        [SerializeField] private string _opposingId;
        [SerializeField] private Vector2 _exitDirection;

        private Action<PlayerBehaviour, DoorBehaviour> _playerExitedThroughDoor = (player, door) => { };
        private Action<PlayerBehaviour, DoorBehaviour> _playerEnteredThroughDoor = (player, door) => { };

        public override Id<DoorBehaviour> Id {
            get { return new Id<DoorBehaviour>(new Guid(_id)); }
        }

        public override Id<DoorBehaviour> OpposingDoorId {
            get { return new Id<DoorBehaviour>(new Guid(_opposingId)); }
        }

        public override RoomBehaviour Room {
            get { return transform.parent.GetComponent<RoomBehaviour>(); }
        }

        public override Vector2 AnchorPosition {
            get { return transform.position; }
        }

        public override Vector2 ExitDirection {
            get { return _exitDirection; }
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
            var roomManager = GetComponentInParent<RoomManagerBehaviour>();

            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null && Room == roomManager.CurrentRoom) {
                PlayerExitedThroughDoor(player, this);
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            var roomManager = GetComponentInParent<RoomManagerBehaviour>();

            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null && Room != roomManager.CurrentRoom) {
                PlayerEnteredThroughDoor(player, this);
            }
        }
    }
}