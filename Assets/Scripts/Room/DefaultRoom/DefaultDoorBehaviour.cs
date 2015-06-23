using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    [RequireComponent(typeof (BoxCollider2D))]
    public class DefaultDoorBehaviour : DoorBehaviour {
        [SerializeField] private string _id;
        [SerializeField] private string _opposingId;
        [SerializeField] private Vector2 _anchorLocalPosition;

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
            get {
                Vector2 position = transform.position;
                return _anchorLocalPosition + position;
            }
        }

        public override Action<DoorBehaviour> PlayerEnteredThroughDoor { get; set; }

        public override Action<DoorBehaviour> PlayerExitedThroughDoor { get; set; }

        public void OnTriggerEnter2D(Collider2D other) {
            var roomManager = GetComponentInParent<RoomManagerBehaviour>();

            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null && Room == roomManager.CurrentRoom) {
                PlayerExitedThroughDoor(this);
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            var roomManager = GetComponentInParent<RoomManagerBehaviour>();

            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (player != null && Room != roomManager.CurrentRoom) {
                PlayerEnteredThroughDoor(this);
            }
        }
    }
}