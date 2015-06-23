using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    using System.Linq;

    public class DefaultRoomBehaviour : RoomBehaviour {
        [SerializeField] private string _id;

        [SerializeField] private Vector2 _roomSize;

        public override Id<RoomBehaviour> Id {
            get { return new Id<RoomBehaviour>(new Guid(_id)); }
        }

        public override Rect Area {
            get {
                Vector2 position = transform.position;
                var topLeft = position - (_roomSize / 2);
                return new Rect(topLeft.x, topLeft.y, _roomSize.x, _roomSize.y);
            }
        }

        public override ReadOnlyCollection<DoorBehaviour> Doors {
            get {
                var doors = gameObject.GetComponentsInChildren<DoorBehaviour>(true);
                return new List<DoorBehaviour>(doors).AsReadOnly();
            }
        }

        public override DoorBehaviour GetDoor(Id<DoorBehaviour> doorId) {
            return Doors.Single(door => doorId.Equals(door.Id));
        }

        public override void Activate() {
            gameObject.SetActive(true);
        }

        public override void Deactivate() {
            gameObject.SetActive(false);
        }
    }
}